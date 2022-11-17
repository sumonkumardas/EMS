using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.AspNetCore.Mvc;
using SinePulse.EMS.Constants;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Messages.BillingMessages;
using SinePulse.EMS.Messages.OnlinePaymentMessages;
using SinePulse.EMS.Messages.SmsSendingMessages;
using SinePulse.EMS.Site.Models;
using SinePulse.EMS.Site.Presenters;
using SinePulse.EMS.UseCases.Authorization;
using SinePulse.EMS.UseCases.Billings;
using SinePulse.EMS.UseCases.OnlinePayments;
using SinePulse.EMS.Utility.MessagingQueue;
using SinePulse.EMS.Utility.SslPaymentGateway;

namespace SinePulse.EMS.Site.Controllers
{
  public class OnlinePaymentController : BaseController
  {
    private readonly GetUnpaidYearsRequestHandler _getUnpaidYearsRequestHandler;
    private readonly GetUnpaidMonthsRequestHandler _getUnpaidMonthsRequestHandler;
    private readonly GetDueAmountRequestHandler _getDueAmountRequestHandler;
    private readonly GetUserInfoRequestHandler _getUserInfoRequestHandler;
    private readonly AddPaymentRequestHandler _addPaymentRequestHandler;
    private readonly AddPaymentResponsePresenter _addPaymentResponsePresenter;
    private readonly IPaymentHelper _paymentHelper;
    private readonly GetPaymentInfoRequestHandler _getPaymentInfoRequestHandler;
    private readonly UpdateTransactionInfoRequestHandler _updateTransactionInfoRequestHandler;
    private readonly AddBillingDataRequestHandler _addBillingDataRequestHandler;
    private readonly DeleteFailedTransactionRequestHandler _deleteFailedTransactionRequestHandler;
    private readonly IMessageSender _messageSender;

    public OnlinePaymentController(GetUserAssociationRequestHandler getUserAssociationRequestHandler,
      GetUnpaidYearsRequestHandler getUnpaidYearsRequestHandler,
      GetUnpaidMonthsRequestHandler getUnpaidMonthsRequestHandler,
      GetDueAmountRequestHandler getDueAmountRequestHandler,
      GetUserInfoRequestHandler getUserInfoRequestHandler,
      AddPaymentRequestHandler addPaymentRequestHandler,
      AddPaymentResponsePresenter addPaymentResponsePresenter,
      IPaymentHelper paymentHelper,
      GetPaymentInfoRequestHandler getPaymentInfoRequestHandler,
      UpdateTransactionInfoRequestHandler updateTransactionInfoRequestHandler,
      AddBillingDataRequestHandler addBillingDataRequestHandler,
      DeleteFailedTransactionRequestHandler deleteFailedTransactionRequestHandler,
      IMessageSender messageSender) : base(getUserAssociationRequestHandler)
    {
      _getUnpaidYearsRequestHandler = getUnpaidYearsRequestHandler;
      _getUnpaidMonthsRequestHandler = getUnpaidMonthsRequestHandler;
      _getDueAmountRequestHandler = getDueAmountRequestHandler;
      _getUserInfoRequestHandler = getUserInfoRequestHandler;
      _addPaymentRequestHandler = addPaymentRequestHandler;
      _addPaymentResponsePresenter = addPaymentResponsePresenter;
      _paymentHelper = paymentHelper;
      _getPaymentInfoRequestHandler = getPaymentInfoRequestHandler;
      _updateTransactionInfoRequestHandler = updateTransactionInfoRequestHandler;
      _addBillingDataRequestHandler = addBillingDataRequestHandler;
      _deleteFailedTransactionRequestHandler = deleteFailedTransactionRequestHandler;
      _messageSender = messageSender;
    }

    private List<int> GetUnpaidYears(long branchMediumId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new GetUnpaidYearsRequestMessage {BranchMediumId = branchMediumId};
      var response = _getUnpaidYearsRequestHandler.Handle(requestMessage, cancellationToken);
      return response.Result.Years;
    }

    [HttpGet]
    public ActionResult PayBill(long branchMediumId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new GetDueAmountRequestMessage
      {
        BranchMediumId = branchMediumId
      };
      var response = _getDueAmountRequestHandler.Handle(requestMessage, cancellationToken);

      return View(new PayBillViewModel
        {
          Years = GetUnpaidYears(branchMediumId),
          BranchMediumId = branchMediumId,
          PerStudentCharge = response.Result.PendingInfos.PerStudentCharge,
          TotalActiveStudent = response.Result.PendingInfos.ActiveStudents,
          DueAmount = response.Result.PendingInfos.PendingAmount
        }
      );
    }

    [HttpPost]
    public ActionResult PayBill(PayBillViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new GetUserInfoRequestMessage
      {
        EmployeeCode = HttpContext.User.Identity.Name
      };
      var userInfo = _getUserInfoRequestHandler.Handle(requestMessage, cancellationToken).Result.UserInfos;
      var consumerName = userInfo.Fullname;
      var consumerPhoneNo = userInfo.PhoneNo;
      var consumerEmail = userInfo.EmailAddress;
      var transactionId = Guid.NewGuid().ToString();

      var addPaymentRequestMessage = new AddPaymentRequestMessage
      {
        BranchMediumId = model.BranchMediumId,
        TransactionId = transactionId,
        UserCode = HttpContext.User.Identity.Name,
        Year = model.Year,
        Month = model.Month,
        DueAmount = model.DueAmount,
        PerStudentCharge = Convert.ToDecimal(model.PerStudentCharge),
        TotalStudents = Convert.ToInt32(model.TotalActiveStudent),
        CurrentUserName = HttpContext.User.Identity.Name
      };

      var addPaymentResponse = _addPaymentRequestHandler.Handle(addPaymentRequestMessage, cancellationToken);
      var viewModel = _addPaymentResponsePresenter.Handle(addPaymentResponse.Result, model, ModelState);
      if (addPaymentResponse.Result.ValidationResult.IsValid)
      {
        var baseUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";
        var paymentGatewayUrl = _paymentHelper.GetPaymentGatewayUrl(baseUrl, model.DueAmount.ToString("0.00"), transactionId,
          consumerName, consumerEmail, consumerPhoneNo);
        Response.Redirect(paymentGatewayUrl);
      }

      viewModel.BranchMediumId = model.BranchMediumId;
      viewModel.Years = GetUnpaidYears(model.BranchMediumId);
      return View(viewModel);
    }

    private void SendTransactionSuccessSmsToUser(string transactionId, string paymentInfoUserCode)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new GetUserInfoRequestMessage();
      requestMessage.EmployeeCode = paymentInfoUserCode;
      var userInfo = _getUserInfoRequestHandler.Handle(requestMessage, cancellationToken).Result.UserInfos;

      var paymentInfoRequestMessage = new GetPaymentInfoRequestMessage {TransactionId = transactionId};
      var paymentInfo = _getPaymentInfoRequestHandler.Handle(paymentInfoRequestMessage, cancellationToken).Result
        .PaymentInfos;
      var smsMessage = new SmsMessage
      {
        PhoneNumbers = new List<string> {userInfo.PhoneNo},
        Message =
          $"Dear Consumer, Payment for {paymentInfo.Month:G}, {paymentInfo.Year} BDT {paymentInfo.TransactionAmount:0.00} is successful. TrxID {paymentInfo.TransactionId}.\n- SinePulseEms"
      };
      _messageSender.Send(smsMessage, MicroServiceAddresses.SmsSendingService);
    }

    private void AddBillingData(string transactionId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new GetPaymentInfoRequestMessage {TransactionId = transactionId};
      var paymentInfo = _getPaymentInfoRequestHandler.Handle(requestMessage, cancellationToken).Result.PaymentInfos;
      var addBillingDataRequestMessage = new AddBillingDataRequestMessage
      {
        BranchMediumId = paymentInfo.BranchMediumId,
        TransactionId = paymentInfo.TransactionId,
        UserCode = paymentInfo.UserCode,
        Year = paymentInfo.Year,
        Month = paymentInfo.Month,
        PaymentDate = paymentInfo.PaymentDate,
        PerStudentCharge = paymentInfo.PerStudentCharge,
        TotalStudents = paymentInfo.TotalStudents,
        Amount = paymentInfo.TransactionAmount,
        PaymentMethod = PaymentMethod.ByCreditCard,
        IsProcessed = true,
        CurrentUserName = paymentInfo.UserCode
      };
      var response = _addBillingDataRequestHandler.Handle(addBillingDataRequestMessage, cancellationToken);
    }

    private void UpdateTransactionInfo(string transactionId, string storeAmount, string validationId, string cardType,
      string paymentDate, string paymentInfoUserCode)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new UpdateTransactionInfoRequestMessage
      {
        CurrentUserName = paymentInfoUserCode,
        TransactionId = transactionId,
        CardType = cardType,
        PaymentDate = DateTime.Parse(paymentDate),
        StoreAmount = Convert.ToDecimal(storeAmount),
        ValidationId = validationId
      };
      var response = _updateTransactionInfoRequestHandler.Handle(requestMessage, cancellationToken);
    }

    public ActionResult TransactionSuccess(string transactionId)
    {
      if (!String.IsNullOrEmpty(Request.Form["status"]) && Request.Form["status"] == "VALID")
      {
        var cancellationToken = new CancellationToken();
        var requestMessage = new GetPaymentInfoRequestMessage {TransactionId = transactionId};
        var paymentInfo = _getPaymentInfoRequestHandler.Handle(requestMessage, cancellationToken).Result.PaymentInfos;
        var amount = paymentInfo.TransactionAmount.ToString("0.00");
        var currency = "BDT";
        var sslCommerz = new SSLCommerz(SslPaymentData.TestStoreId, SslPaymentData.TestStorePassword,
          SslPaymentData.PaymentTestMode);
        var storeAmount = Request.Form["store_amount"];
        var validationId = Request.Form["val_id"];
        var cardType = Request.Form["card_type"];
        var paymentDate = Request.Form["tran_date"];
        var valid = sslCommerz.OrderValidate(transactionId, amount, currency, Request);
        if (valid)
        {
          UpdateTransactionInfo(transactionId, storeAmount, validationId, cardType, paymentDate, paymentInfo.UserCode);
          AddBillingData(paymentInfo.TransactionId);
          SendTransactionSuccessSmsToUser(transactionId, paymentInfo.UserCode);
          var successViewModel = new TransactionSuccessViewModel();
          successViewModel.BranchMediumId = paymentInfo.BranchMediumId;
          successViewModel.TransactionAmount = paymentInfo.TransactionAmount;
          successViewModel.TransactionId = transactionId;
          successViewModel.PaymentFor = paymentInfo.Month.ToString("G") + ", " + paymentInfo.Year;
          return View(successViewModel);
        }
        else
        {
          return RedirectToAction("TransactionFailure", new {transactionId});
        }
      }

      return View();
    }

    public ActionResult TransactionFailure(string transactionId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new GetPaymentInfoRequestMessage {TransactionId = transactionId};
      var paymentInfo = _getPaymentInfoRequestHandler.Handle(requestMessage, cancellationToken).Result.PaymentInfos;
      var viewModel = new TransactionFailureVieModel();
      viewModel.BranchMediumId = paymentInfo.BranchMediumId;
      viewModel.TransactionAmount = paymentInfo.TransactionAmount;
      viewModel.TransactionId = transactionId;
      DeleteTransactionInfo(transactionId);
      return View(viewModel);
    }

    public ActionResult TransactionCanceled(string transactionId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new GetPaymentInfoRequestMessage {TransactionId = transactionId};
      var paymentInfo = _getPaymentInfoRequestHandler.Handle(requestMessage, cancellationToken).Result.PaymentInfos;
      var branchMediumId = paymentInfo.BranchMediumId;
      DeleteTransactionInfo(transactionId);
      return RedirectToAction("PayBill", new {branchMediumId});
    }

    private void DeleteTransactionInfo(string transactionId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new DeleteFailedTransactionRequestMessage {TransactionId = transactionId};
      var response = _deleteFailedTransactionRequestHandler.Handle(requestMessage, cancellationToken);
    }

    public JsonResult GetUnpaidMonths(long branchMediumId, int year)
    {
      if (branchMediumId > 0 && year > 0)
      {
        var cancellationToken = new CancellationToken();
        var requestMessage = new GetUnpaidMonthsRequestMessage
        {
          BranchMediumId = branchMediumId,
          Year = year
        };
        var response = _getUnpaidMonthsRequestHandler.Handle(requestMessage, cancellationToken);
        return Json(response.Result.Months);
      }

      return Json(null);
    }
  }
}