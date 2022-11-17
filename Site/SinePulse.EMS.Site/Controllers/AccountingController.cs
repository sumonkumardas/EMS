using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.AspNetCore.Mvc;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Messages.AccountHeadMessages;
using SinePulse.EMS.Messages.AutoPostingConfigurationMessages;
using SinePulse.EMS.Messages.BalanceSheetMessages;
using SinePulse.EMS.Messages.BranchMediumAccountsHeadMessages;
using SinePulse.EMS.Messages.BranchMediumMessages;
using SinePulse.EMS.Messages.IncomeStatementMessages;
using SinePulse.EMS.Messages.Model.Accounts;
using SinePulse.EMS.Messages.SessionMessages;
using SinePulse.EMS.Messages.TransactionMessages;
using SinePulse.EMS.Messages.TrialBalanceMessages;
using SinePulse.EMS.ProjectPrimitives;
using SinePulse.EMS.Site.Models;
using SinePulse.EMS.Site.Presenters;
using SinePulse.EMS.UseCases.AccountHeads;
using SinePulse.EMS.UseCases.AutoPostingConfigurations;
using SinePulse.EMS.UseCases.BalanceSheets;
using SinePulse.EMS.UseCases.BranchMediumAccountsHeads;
using SinePulse.EMS.UseCases.BranchMediums;
using SinePulse.EMS.UseCases.IncomeStatements;
using SinePulse.EMS.UseCases.Sessions;
using SinePulse.EMS.UseCases.Transactions;
using SinePulse.EMS.UseCases.TrialBalances;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.UseCases.Authorization;
using VoucherTypeEnum = SinePulse.EMS.Messages.Model.Enums.VoucherTypeEnum;
using Microsoft.AspNetCore.Authorization;
using SinePulse.EMS.Site.Authorization;
using SinePulse.EMS.Domain.Features;

namespace SinePulse.EMS.Site.Controllers
{
  [Authorize]
  public class AccountingController : BaseController
  {
    private readonly ShowAccountHeadListRequestHandler _showAccountHeadListRequestHandler;
    private readonly ShowAccountHeadListResponsePresenter _showAccountHeadListResponsePresenter;
    private readonly ShowAccountHeadRequestHandler _showAccountHeadRequestHandler;
    private readonly ShowAccountHeadResponsePresenter _showAccountHeadPresenter;
    private readonly ShowBranchMediumRequestHandler _showBranchMediumRequestHandler;
    private readonly ShowBranchMediumResponsePresenter _showBranchMediumResponsePresenter;
    private readonly ShowBranchMediumAccountsHeadListRequestHandler _showBranchMediumAccountsHeadListRequestHandler;
    private readonly ShowBranchMediumAccountsHeadListResponsePresenter _showBranchMediumAccountsHeadListResponsePresenter;
    private readonly AddJournalTransactionRequestHandler _addJournalTransactionRequestHandler;
    private readonly AddCashVoucherJournalTransactionRequestHandler _addCashVoucherJournalTransactionRequestHandler;
    private readonly AddBankVoucherJournalTransactionRequestHandler _addBankVoucherJournalTransactionRequestHandler;
    private readonly ShowAutoPostingConfigurationListRequestHandler _showAutoPostingConfigurationListRequestHandler;
    private readonly ShowBankAccountChildListResponsePresenter _showBankAccountChildListResponsePresenter;
    private readonly ShowTrialBalanceRequestHandler _showTrialBalanceRequestHandler;
    private readonly ShowTrialBalanceTransactionResponsePresenter _showTrialBalanceTransactionResponsePresenter;
    private readonly ShowTransactionRequestHandler _showTransactionRequestHandler;
    private readonly ShowTransactionResponsePresenter _showTransactionResponsePresenter;
    private readonly ShowIncomeStatementRequestHandler _showIncomeStatementRequestHandler;
    private readonly ShowIncomeStatementTransactionResponsePresenter
        _showIncomeStatementTransactionResponsePresenter;

    private readonly ShowBalanceSheetRequestHandler _showBalanceSheetRequestHandler;
    private readonly ShowBalanceSheetTransactionResponsePresenter _showBalanceSheetTransactionResponsePresenter;
    private readonly ShowBranchMediumAccountHeadRequestHandler _showBranchMediumAccountHeadRequestHandler;
    private readonly AddBranchMediumAccountHeadRequestHandler _addBranchMediumAccountHeadRequestHandler;
    private readonly ShowSessionRequestHandler _showSessionRequestHandler;
    private readonly ShowTransactionListRequestHandler _showTransactionListRequestHandler;
    private readonly ShowTransactionListResponsePresenter _showTransactionListResponsePresenter;
    private readonly ICurrentSessionProvider _currentSessionProvider;
    private readonly DisplayBranchMediumRequestHandler _displayBranchMediumRequestHandler;
    private readonly AccountDisplayResponsePresenter _accountDisplayResponsePresenter;
    public AccountingController(ShowAccountHeadListRequestHandler showAccountHeadListRequestHandler,
      ShowAccountHeadListResponsePresenter showAccountHeadListResponsePresenter,
      ShowAccountHeadRequestHandler showAccountHeadRequestHandler,
      ShowAccountHeadResponsePresenter showAccountHeadPresenter,
      ShowBranchMediumRequestHandler showBranchMediumRequestHandler,
      ShowBranchMediumResponsePresenter showBranchMediumResponsePresenter,
      ShowBranchMediumAccountsHeadListRequestHandler showBranchMediumAccountsHeadListRequestHandler,
      ShowBranchMediumAccountsHeadListResponsePresenter showBranchMediumAccountsHeadListResponsePresenter,
      AddJournalTransactionRequestHandler addJournalTransactionRequestHandler,
      AddCashVoucherJournalTransactionRequestHandler addCashVoucherJournalTransactionRequestHandler,
      AddBankVoucherJournalTransactionRequestHandler addBankVoucherJournalTransactionRequestHandler,
      ShowAutoPostingConfigurationListRequestHandler showAutoPostingConfigurationListRequestHandler,
      ShowBankAccountChildListResponsePresenter showBankAccountChildListResponsePresenter,
      ShowTrialBalanceRequestHandler showTrialBalanceRequestHandler,
      ShowTrialBalanceTransactionResponsePresenter showTrialBalanceTransactionResponsePresenter,
      ShowTransactionRequestHandler showTransactionRequestHandler,
      ShowTransactionResponsePresenter showTransactionResponsePresenter,
      ShowIncomeStatementRequestHandler showIncomeStatementRequestHandler,
      ShowIncomeStatementTransactionResponsePresenter
          showIncomeStatementTransactionResponsePresenter,
      ShowBalanceSheetRequestHandler showBalanceSheetRequestHandler,
      ShowBalanceSheetTransactionResponsePresenter showBalanceSheetTransactionResponsePresenter,
      ShowBranchMediumAccountHeadRequestHandler showBranchMediumAccountHeadRequestHandler,
      AddBranchMediumAccountHeadRequestHandler addBranchMediumAccountHeadRequestHandler,
      ShowSessionRequestHandler showSessionRequestHandler,
      ShowTransactionListRequestHandler showTransactionListRequestHandler,
      ShowTransactionListResponsePresenter showTransactionListResponsePresenter,
      ICurrentSessionProvider currentSessionProvider,
      DisplayBranchMediumRequestHandler displayBranchMediumRequestHandler,
      AccountDisplayResponsePresenter accountDisplayResponsePresenter,
      GetUserAssociationRequestHandler getUserAssociationRequestHandler) : base(getUserAssociationRequestHandler)
    {
      _showAccountHeadListRequestHandler = showAccountHeadListRequestHandler;
      _showAccountHeadListResponsePresenter = showAccountHeadListResponsePresenter;
      _showAccountHeadRequestHandler = showAccountHeadRequestHandler;
      _showAccountHeadPresenter = showAccountHeadPresenter;
      _showBranchMediumRequestHandler = showBranchMediumRequestHandler;
      _showBranchMediumResponsePresenter = showBranchMediumResponsePresenter;
      _showBranchMediumAccountsHeadListRequestHandler = showBranchMediumAccountsHeadListRequestHandler;
      _showBranchMediumAccountsHeadListResponsePresenter = showBranchMediumAccountsHeadListResponsePresenter;
      _addJournalTransactionRequestHandler = addJournalTransactionRequestHandler;
      _addCashVoucherJournalTransactionRequestHandler = addCashVoucherJournalTransactionRequestHandler;
      _addBankVoucherJournalTransactionRequestHandler = addBankVoucherJournalTransactionRequestHandler;
      _showAutoPostingConfigurationListRequestHandler = showAutoPostingConfigurationListRequestHandler;
      _showBankAccountChildListResponsePresenter = showBankAccountChildListResponsePresenter;
      _showTrialBalanceRequestHandler = showTrialBalanceRequestHandler;
      _showTrialBalanceTransactionResponsePresenter = showTrialBalanceTransactionResponsePresenter;
      _showTransactionRequestHandler = showTransactionRequestHandler;
      _showTransactionResponsePresenter = showTransactionResponsePresenter;
      _showIncomeStatementRequestHandler = showIncomeStatementRequestHandler;
      _showIncomeStatementTransactionResponsePresenter = showIncomeStatementTransactionResponsePresenter;
      _showBalanceSheetRequestHandler = showBalanceSheetRequestHandler;
      _showBalanceSheetTransactionResponsePresenter = showBalanceSheetTransactionResponsePresenter;
      _showBranchMediumAccountHeadRequestHandler = showBranchMediumAccountHeadRequestHandler;
      _addBranchMediumAccountHeadRequestHandler = addBranchMediumAccountHeadRequestHandler;
      _showSessionRequestHandler = showSessionRequestHandler;
      _showTransactionListRequestHandler = showTransactionListRequestHandler;
      _showTransactionListResponsePresenter = showTransactionListResponsePresenter;
      _currentSessionProvider = currentSessionProvider;
      _displayBranchMediumRequestHandler = displayBranchMediumRequestHandler;
      _accountDisplayResponsePresenter = accountDisplayResponsePresenter;
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Accounting, (int)Feature.AccountingEnum.GeneralJournal)]
    [HttpGet]
    public ActionResult AddGeneralJournal(long branchMediumId)
    {
      var cancellationToken = new CancellationToken();
      AddGeneralJournalTransactionViewModel model = new AddGeneralJournalTransactionViewModel();
      GetUserAssociationResponseMessage userAssociationMessage = GetUserAssociationResponseMessage();

      model.LoginName = userAssociationMessage.LoginName;
      model.AssociatedWith = userAssociationMessage.AssociatedWith;
      model.LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId;
      model.LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId;
      model.LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId;
      model.RoleFeatures = userAssociationMessage.RoleFeatures;
      model.InstituteBanner = userAssociationMessage.InstituteBanner;

      model.BranchMediumId = branchMediumId;
      var requestMessage = new ShowBranchMediumRequestMessage();
      requestMessage.BranchMediumId = branchMediumId;
      ShowBranchMediumViewModel showBranchMediumViewModel = new ShowBranchMediumViewModel();
      var response = _showBranchMediumRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _showBranchMediumResponsePresenter.Handle(response.Result, showBranchMediumViewModel, GetUserAssociationResponseMessage());
      model.SessionId = viewModel.BranchMedium.Sessions.FirstOrDefault().Id;

      return View(model);
    }
    [HttpPost]
    public JsonResult AddGeneralJournal([FromBody]AddGeneralJournalTransactionViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new AddJournalTransactionRequestMessage
      {
        TransactionDate = DateTime.Parse(model.TransactionDate),
        Description = model.Description,
        TransactionEntries = model.TransactionEntries.Select(e =>
            new AddJournalTransactionRequestMessage.TransactionEntry
            {
              AccountHeadId = e.AccountHeadId,
              DebitAmount = e.DebitAmount,
              CreditAmount = e.CreditAmount
            }).ToList(),
        BranchMediumId = model.BranchMediumId,
        SessionId = model.SessionId,
        isContraTransanction = false,
        CurrentUserName = HttpContext.User.Identity.Name
      };

      var response = _addJournalTransactionRequestHandler.Handle(requestMessage, cancellationToken);
      GetUserAssociationResponseMessage userAssociationMessage = GetUserAssociationResponseMessage();
      var viewModel = new JournalTransactionResponseViewModel();
      viewModel.BranchMediumId = model.BranchMediumId;

      viewModel.LoginName = userAssociationMessage.LoginName;
      viewModel.LoginImageUrl = userAssociationMessage.ImageUrl;
      viewModel.AssociatedWith = userAssociationMessage.AssociatedWith;
      viewModel.LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId;
      viewModel.LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId;
      viewModel.LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId;
      viewModel.RoleFeatures = userAssociationMessage.RoleFeatures;
      viewModel.InstituteBanner = userAssociationMessage.InstituteBanner;

      if (response.Result.ValidationResult.IsValid)
      {
        viewModel.IsDataPosted = true;
        viewModel.ErrorMessages = new List<string>();
        viewModel.RedirectTo = Url.Action("ShowTransaction", "Accounting",
            new { transactionId = response.Result.Data.TransactionId, branchMediumId = model.BranchMediumId });
      }
      else
      {
        viewModel.IsDataPosted = false;
        viewModel.ErrorMessages = new List<string>();
        viewModel.RedirectTo = null;
      }

      return Json(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Accounting, (int)Feature.AccountingEnum.ContraJournal)]
    [HttpGet]
    public ActionResult AddContraJournal(long branchMediumId)
    {
      var cancellationToken = new CancellationToken();
      var model = new AddContraJournalTransactionViewModel { BranchMediumId = branchMediumId };
      GetUserAssociationResponseMessage userAssociationMessage = GetUserAssociationResponseMessage();

      model.LoginName = userAssociationMessage.LoginName;
      model.AssociatedWith = userAssociationMessage.AssociatedWith;
      model.LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId;
      model.LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId;
      model.LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId;
      model.RoleFeatures = userAssociationMessage.RoleFeatures;
      model.InstituteBanner = userAssociationMessage.InstituteBanner;

      var requestMessage = new ShowBranchMediumRequestMessage { BranchMediumId = branchMediumId };
      var showBranchMediumViewModel = new ShowBranchMediumViewModel();
      var response = _showBranchMediumRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _showBranchMediumResponsePresenter.Handle(response.Result, showBranchMediumViewModel, GetUserAssociationResponseMessage());
      model.SessionId = viewModel.BranchMedium.Sessions.FirstOrDefault().Id;

      return View(model);
    }
    [HttpPost]
    public JsonResult AddContraJournal([FromBody]AddContraJournalTransactionViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new AddJournalTransactionRequestMessage
      {
        TransactionDate = DateTime.Parse(model.TransactionDate),
        Description = model.Description,
        TransactionEntries = model.TransactionEntries.Select(e =>
            new AddJournalTransactionRequestMessage.TransactionEntry
            {
              AccountHeadId = e.AccountHeadId,
              DebitAmount = e.DebitAmount,
              CreditAmount = e.CreditAmount
            }).ToList(),
        BranchMediumId = model.BranchMediumId,
        SessionId = model.SessionId,
        isContraTransanction = true,
        CurrentUserName = HttpContext.User.Identity.Name
      };


      var response = _addJournalTransactionRequestHandler.Handle(requestMessage, cancellationToken);
      GetUserAssociationResponseMessage userAssociationMessage = GetUserAssociationResponseMessage();
      var viewModel = new JournalTransactionResponseViewModel();
      viewModel.BranchMediumId = model.BranchMediumId;
      viewModel.LoginName = userAssociationMessage.LoginName;
      viewModel.LoginImageUrl = userAssociationMessage.ImageUrl;
      viewModel.AssociatedWith = userAssociationMessage.AssociatedWith;
      viewModel.LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId;
      viewModel.LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId;
      viewModel.LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId;
      viewModel.RoleFeatures = userAssociationMessage.RoleFeatures;
      viewModel.InstituteBanner = userAssociationMessage.InstituteBanner;

      if (response.Result.ValidationResult.IsValid)
      {
        viewModel.IsDataPosted = true;
        viewModel.ErrorMessages = new List<string>();
        viewModel.RedirectTo = Url.Action("ShowTransaction", "Accounting",
            new { transactionId = response.Result.Data.TransactionId, branchMediumId = model.BranchMediumId });
      }
      else
      {
        viewModel.IsDataPosted = false;
        viewModel.ErrorMessages = new List<string>();
        viewModel.RedirectTo = null;
      }
      return Json(viewModel);
    }
    [HttpGet]

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Accounting, (int)Feature.AccountingEnum.CashVoucher)]
    public ActionResult AddCashVoucher(long branchMediumId)
    {
      var cancellationToken = new CancellationToken();
      AddCashVoucherViewModel model = new AddCashVoucherViewModel { BranchMediumId = branchMediumId };
      GetUserAssociationResponseMessage userAssociationMessage = GetUserAssociationResponseMessage();

      model.LoginName = userAssociationMessage.LoginName;
      model.AssociatedWith = userAssociationMessage.AssociatedWith;
      model.LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId;
      model.LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId;
      model.LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId;
      model.RoleFeatures = userAssociationMessage.RoleFeatures;
      model.InstituteBanner = userAssociationMessage.InstituteBanner;

      var requestMessage = new ShowBranchMediumRequestMessage
      {
        BranchMediumId = branchMediumId
      };
      var showBranchMediumViewModel = new ShowBranchMediumViewModel();
      var response = _showBranchMediumRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _showBranchMediumResponsePresenter.Handle(response.Result, showBranchMediumViewModel, GetUserAssociationResponseMessage());
      if (viewModel != null) model.SessionId = viewModel.BranchMedium.Sessions.FirstOrDefault().Id;

      return View(model);
    }
    [HttpPost]
    public JsonResult AddCashVoucher([FromBody]AddCashVoucherViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new AddCashVoucherJournalTransactionRequestMessage
      {
        TransactionDate = model.TransactionDate,
        Description = model.Description,
        IsDebitTransaction = model.TransactionEntryType == TransactionEntryType.Debit,
        TransactionEntries = model.TransactionEntries.Select(e =>
            new AddCashVoucherJournalTransactionRequestMessage.TransactionEntry
            {
              AccountHeadId = e.AccountHeadId,
              Amount = e.Amount
            }).ToList(),
        BranchMediumId = model.BranchMediumId,
        SessionId = model.SessionId,
        CurrentUserName = HttpContext.User.Identity.Name
      };


      var response = _addCashVoucherJournalTransactionRequestHandler.Handle(requestMessage, cancellationToken);
      GetUserAssociationResponseMessage userAssociationMessage = GetUserAssociationResponseMessage();
      var viewModel = new JournalTransactionResponseViewModel();
      viewModel.BranchMediumId = model.BranchMediumId;
      viewModel.LoginName = userAssociationMessage.LoginName;
      viewModel.LoginImageUrl = userAssociationMessage.ImageUrl;
      viewModel.AssociatedWith = userAssociationMessage.AssociatedWith;
      viewModel.LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId;
      viewModel.LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId;
      viewModel.LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId;
      viewModel.RoleFeatures = userAssociationMessage.RoleFeatures;
      viewModel.InstituteBanner = userAssociationMessage.InstituteBanner;

      if (response.Result.ValidationResult.IsValid)
      {
        viewModel.IsDataPosted = true;
        viewModel.ErrorMessages = new List<string>();
        viewModel.RedirectTo = Url.Action("ShowTransaction", "Accounting",
            new { transactionId = response.Result.Data.TransactionId, branchMediumId = model.BranchMediumId });
      }
      else
      {
        viewModel.IsDataPosted = false;
        viewModel.ErrorMessages = new List<string>();
        viewModel.RedirectTo = null;
      }
      return Json(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Accounting, (int)Feature.AccountingEnum.BankVoucher)]
    [HttpGet]
    public ActionResult AddBankVoucher(long branchMediumId)
    {
      var cancellationToken = new CancellationToken();
      GetUserAssociationResponseMessage userAssociationMessage = GetUserAssociationResponseMessage();
      AddBankVoucherViewModel model = new AddBankVoucherViewModel { BranchMediumId = branchMediumId };
      model.LoginName = userAssociationMessage.LoginName;
      model.AssociatedWith = userAssociationMessage.AssociatedWith;
      model.LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId;
      model.LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId;
      model.LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId;
      model.RoleFeatures = userAssociationMessage.RoleFeatures;
      model.InstituteBanner = userAssociationMessage.InstituteBanner;

      var requestMessage = new ShowBranchMediumRequestMessage
      {
        BranchMediumId = branchMediumId
      };
      var showBranchMediumViewModel = new ShowBranchMediumViewModel();
      var response = _showBranchMediumRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _showBranchMediumResponsePresenter.Handle(response.Result, showBranchMediumViewModel, GetUserAssociationResponseMessage());
      if (viewModel != null) model.SessionId = viewModel.BranchMedium.Sessions.FirstOrDefault().Id;

      var autoPostingConfigurationListRequestMessage = new ShowAutoPostingConfigurationListRequestMessage()
      {
        VoucherType = VoucherTypeEnum.BankVoucher
      };
      var autoPostingConfigurationListResponse = _showAutoPostingConfigurationListRequestHandler.Handle(autoPostingConfigurationListRequestMessage, cancellationToken);

      var branchAccountHeadRequestMessage = new ShowBranchMediumAccountsHeadListRequestMessage
      {
        BranchMediumId = branchMediumId,
        ParentAccountHeadCode = autoPostingConfigurationListResponse.Result.AutoPostingConfigurations.FirstOrDefault()?.AccountCode,
        CurrentSessionId = model.SessionId
      };
      var branchAccountHeadResponseMessage =
          _showBranchMediumAccountsHeadListRequestHandler.Handle(branchAccountHeadRequestMessage,
              cancellationToken);

      model.BankAccountChilds =
          _showBankAccountChildListResponsePresenter.Handle(branchAccountHeadResponseMessage.Result,
              new List<BankAccountChildViewModel>());
      return View(model);
    }
    [HttpPost]
    public JsonResult AddBankVoucher([FromBody]AddBankVoucherViewModel model)
    {
      var cancellationToken = new CancellationToken();
      GetUserAssociationResponseMessage userAssociationMessage = GetUserAssociationResponseMessage();
      model.LoginName = userAssociationMessage.LoginName;
      model.AssociatedWith = userAssociationMessage.AssociatedWith;
      model.LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId;
      model.LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId;
      model.LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId;
      model.RoleFeatures = userAssociationMessage.RoleFeatures;
      model.InstituteBanner = userAssociationMessage.InstituteBanner;
      var requestMessage = new AddBankVoucherJournalTransactionRequestMessage
      {
        TransactionDate = model.TransactionDate,
        Description = model.Description,
        IsDebitTransaction = model.TransactionEntryType == TransactionEntryType.Debit,
        TransactionEntries = model.TransactionEntries.Select(e =>
            new AddBankVoucherJournalTransactionRequestMessage.TransactionEntry
            {
              AccountHeadId = e.AccountHeadId,
              Amount = e.Amount
            }).ToList(),
        BranchMediumId = model.BranchMediumId,
        BankAccountHeadId = model.BankAccountHeadId,
        SessionId = model.SessionId,
        CurrentUserName = HttpContext.User.Identity.Name
      };


      var response = _addBankVoucherJournalTransactionRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = new JournalTransactionResponseViewModel();
      if (response.Result.ValidationResult.IsValid)
      {
        viewModel.IsDataPosted = true;
        viewModel.ErrorMessages = new List<string>();
        viewModel.RedirectTo = Url.Action("ShowTransaction", "Accounting",
          new { transactionId = response.Result.Data.TransactionId, branchMediumId = model.BranchMediumId });
      }
      else
      {
        viewModel.IsDataPosted = false;
        viewModel.ErrorMessages = new List<string>();
        viewModel.RedirectTo = null;
      }
      return Json(viewModel);
    }

    public JsonResult GetTransactionTypeAndAccountType()
    {
      TransactionTypeAndAccountTypeViewModel model = new TransactionTypeAndAccountTypeViewModel();
      return Json(model);
    }
    public JsonResult GetAccountHeads(int rowNumber, long accountType, long branchMediumId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowBranchMediumRequestMessage();
      requestMessage.BranchMediumId = branchMediumId;
      ShowBranchMediumViewModel model = new ShowBranchMediumViewModel();
      var response = _showBranchMediumRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _showBranchMediumResponsePresenter.Handle(response.Result, model, GetUserAssociationResponseMessage());

      var branchCAHeadsRequestMessage = new ShowBranchMediumAccountsHeadListRequestMessage();
      branchCAHeadsRequestMessage.BranchMediumId = branchMediumId;
      branchCAHeadsRequestMessage.CurrentSessionId = viewModel.BranchMedium.Sessions.FirstOrDefault().Id;

      var branchCAHeadsResponse = _showBranchMediumAccountsHeadListRequestHandler.Handle(branchCAHeadsRequestMessage, cancellationToken);
      var comboUI = _showBranchMediumAccountsHeadListResponsePresenter.Handle(branchCAHeadsResponse.Result, accountType, rowNumber);

      return Json(comboUI);
    }

    public JsonResult GetAccountHead(long accountHeadId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowBranchMediumAccountHeadRequestMessage { AccountHeadId = accountHeadId };
      BranchMediumAccountsHeadMessageModel model = new BranchMediumAccountsHeadMessageModel();
      var response = _showBranchMediumAccountHeadRequestHandler.Handle(requestMessage, cancellationToken);
      if (response.Result.ValidationResult.IsValid)
        model = response.Result.AccountHead;
      return Json(model);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Accounting, (int)Feature.AccountingEnum.COAEntry)]
    [HttpGet]
    public ActionResult AddAccountHead(long branchMediumId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowBranchMediumRequestMessage();
      requestMessage.BranchMediumId = branchMediumId;
      ShowBranchMediumViewModel model = new ShowBranchMediumViewModel();
      var response = _showBranchMediumRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _showBranchMediumResponsePresenter.Handle(response.Result, model, GetUserAssociationResponseMessage());

      var branchCAHeadsRequestMessage = new ShowBranchMediumAccountsHeadListRequestMessage();
      branchCAHeadsRequestMessage.BranchMediumId = branchMediumId;
      branchCAHeadsRequestMessage.CurrentSessionId = viewModel.BranchMedium.Sessions.FirstOrDefault().Id;

      var branchCAHeadsResponse = _showBranchMediumAccountsHeadListRequestHandler.Handle(branchCAHeadsRequestMessage, cancellationToken);
      var treeUI = _showBranchMediumAccountsHeadListResponsePresenter.Handle(branchCAHeadsResponse.Result);
      var addAccoundHeadChildModel = new AddAccoundHeadChildViewModel();
      GetUserAssociationResponseMessage userAssociationMessage = GetUserAssociationResponseMessage();
      addAccoundHeadChildModel.LoginName = userAssociationMessage.LoginName;
      addAccoundHeadChildModel.AssociatedWith = userAssociationMessage.AssociatedWith;
      addAccoundHeadChildModel.LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId;
      addAccoundHeadChildModel.LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId;
      addAccoundHeadChildModel.LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId;
      addAccoundHeadChildModel.RoleFeatures = userAssociationMessage.RoleFeatures;
      addAccoundHeadChildModel.InstituteBanner = userAssociationMessage.InstituteBanner;
      addAccoundHeadChildModel.BranchMediumId = branchMediumId;
      addAccoundHeadChildModel.SessionId = branchCAHeadsRequestMessage.CurrentSessionId;
      addAccoundHeadChildModel.TreeUi = treeUI;

      return View(addAccoundHeadChildModel);
    }

    [HttpPost]
    public JsonResult AddAccountHead([FromBody]AddAccoundHeadChildViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new AddBranchMediumAccountHeadRequestMessage
      {
        ParentAccountHeadId = model.ParentAccountHeadId,
        AccountCode = model.AccountCode,
        AccountHeadName = model.AccountHeadName,
        AccountHeadType = model.AccountHeadType,
        AccountPeriod = model.AccountPeriod,
        BranchMediumId = model.BranchMediumId,
        CurrentSessionId = model.SessionId,
        IsLedger = model.IsLedger,
        Status = EMS.Domain.Enums.StatusEnum.Active,
        CurrentUserName = HttpContext.User.Identity.Name
      };


      var response = _addBranchMediumAccountHeadRequestHandler.Handle(requestMessage, cancellationToken);

      var addAccoundHeadChildModel = new AddAccoundHeadChildViewModel();

      if (response.Result.ValidationResult.IsValid)
      {
        addAccoundHeadChildModel.Id = response.Result.AccountHeadId;
      }
      else
      {
        var errorMessage = string.Empty;
        foreach (var validationResultError in response.Result.ValidationResult.Errors)
        {
          errorMessage += validationResultError.ErrorMessage + "\n";
        }

        addAccoundHeadChildModel.ErrorMessage = errorMessage;
      }
      return Json(addAccoundHeadChildModel);


    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Accounting, (int)Feature.AccountingEnum.ShowTransaction)]
    [HttpGet]
    public ActionResult ShowTransaction(long transactionId, long branchMediumId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowTransactionRequestMessage { TransactionId = transactionId };
      TransactionViewModel transactionModel = new TransactionViewModel();
      var response = _showTransactionRequestHandler.Handle(requestMessage, cancellationToken);
      var pickedTransactionModel = _showTransactionResponsePresenter.Handle(response.Result, transactionModel);

      GetUserAssociationResponseMessage userAssociationMessage = GetUserAssociationResponseMessage();
      ShowTransactionViewModel viewModel = new ShowTransactionViewModel
      {
        TransactionViewModel = pickedTransactionModel,
        TransactionBranchMediumId = branchMediumId,
        LoginName = userAssociationMessage.LoginName,
        LoginImageUrl = userAssociationMessage.ImageUrl,
        AssociatedWith = userAssociationMessage.AssociatedWith,
        LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId,
        LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId,
        LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId,
        RoleFeatures = userAssociationMessage.RoleFeatures,
        InstituteBanner = userAssociationMessage.InstituteBanner,
        TotalCredit = pickedTransactionModel.TransactionEntries.Where(x => x.AccountHead.AccountType.TransactionType == AccountTransactionTypeEnum.Credit).Sum(x => x.Amount),
        TotalDebit = pickedTransactionModel.TransactionEntries.Where(x => x.AccountHead.AccountType.TransactionType == AccountTransactionTypeEnum.Debit).Sum(x => x.Amount),
      };
      return View(viewModel);
    }

    [HttpGet]
    public JsonResult ShowTrialBalance(long branchMediumId, long sessionId, int endMonth)
    {
      var cancellationToken = new CancellationToken();
      var sessionRequestMessage = new ViewSessionRequestMessage {SessionId = sessionId, InstituteId = 0};

      var sessionResponse = _showSessionRequestHandler.Handle(sessionRequestMessage, cancellationToken);
      var startDate = sessionResponse.Result.Session.StartDate;
      var endDate = new DateTime(sessionResponse.Result.Session.EndDate.Year, endMonth, DateTime.DaysInMonth(sessionResponse.Result.Session.EndDate.Year, endMonth));

      var requestMessage = new ShowTrialBalanceRequestMessage
      {
        BranchMediumId = branchMediumId,
        StartDate = startDate,
        EndDate = endDate,
        CurrentUserName = HttpContext.User.Identity.Name
      };
      var response = _showTrialBalanceRequestHandler.Handle(requestMessage, cancellationToken);
      GetUserAssociationResponseMessage userAssociationMessage = GetUserAssociationResponseMessage();
      var model = new TrialBalanceViewModel
      {
        BranchMediumId = branchMediumId,
        StartDate = startDate,
        EndDate = endDate,
        LoginName = userAssociationMessage.LoginName,
        AssociatedWith = userAssociationMessage.AssociatedWith,
        LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId,
        LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId,
        LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId,
        RoleFeatures = userAssociationMessage.RoleFeatures,
        InstituteBanner = userAssociationMessage.InstituteBanner
      };

      model = _showTrialBalanceTransactionResponsePresenter.Handle(response.Result.Data, model);
      return Json(model);
    }
    [HttpGet]
    public JsonResult ShowIncomeStatement(long branchMediumId, long sessionId, int endMonth)
    {
      var cancellationToken = new CancellationToken();
      var sessionRequestMessage = new ViewSessionRequestMessage();
      sessionRequestMessage.SessionId = sessionId;
      sessionRequestMessage.InstituteId = 0;

      var sessionResponse = _showSessionRequestHandler.Handle(sessionRequestMessage, cancellationToken);
      var startDate = sessionResponse.Result.Session.StartDate;
      var endDate = new DateTime(sessionResponse.Result.Session.EndDate.Year, endMonth, DateTime.DaysInMonth(sessionResponse.Result.Session.EndDate.Year, endMonth));
      var requestMessage = new ShowIncomeStatementRequestMessage
      {
        BranchMediumId = branchMediumId,
        StartDate = startDate,
        EndDate = endDate,
        CurrentUserName = HttpContext.User.Identity.Name
      };
      var response = _showIncomeStatementRequestHandler.Handle(requestMessage, cancellationToken);

      var model = new IncomeStatementViewModel
      {
        StartDate = startDate,
        EndDate = endDate
      };
      model = _showIncomeStatementTransactionResponsePresenter.Handle(response.Result.Data, model);

      return Json(model);
    }

    [HttpGet]
    public JsonResult ShowBalanceSheet(long branchMediumId, long sessionId, int endMonth)
    {
      var cancellationToken = new CancellationToken();
      var sessionRequestMessage = new ViewSessionRequestMessage();
      sessionRequestMessage.SessionId = sessionId;
      sessionRequestMessage.InstituteId = 0;

      var sessionResponse = _showSessionRequestHandler.Handle(sessionRequestMessage, cancellationToken);
      var startDate = sessionResponse.Result.Session.StartDate;
      var endDate = new DateTime(sessionResponse.Result.Session.EndDate.Year, endMonth, DateTime.DaysInMonth(sessionResponse.Result.Session.EndDate.Year, endMonth));
      var requestMessage = new ShowBalanceSheetRequestMessage
      {
        BranchMediumId = branchMediumId,
        StartDate = startDate,
        EndDate = endDate
      };
      var response = _showBalanceSheetRequestHandler.Handle(requestMessage, cancellationToken);

      var model = new BalanceSheetViewModel
      {
        StartDate = startDate,
        EndDate = endDate
      };
      model = _showBalanceSheetTransactionResponsePresenter.Handle(response.Result.Data, model);

      return Json(model);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.FinancialStatements, (int)Feature.FinancialStatementEnum.TrialBalance)]
    [HttpPost]
    public ActionResult ShowTrialBalance(TrialBalanceViewModel model)
    {
      GetUserAssociationResponseMessage userAssociationMessage = GetUserAssociationResponseMessage();
      model.LoginName = userAssociationMessage.LoginName;
      model.AssociatedWith = userAssociationMessage.AssociatedWith;
      model.LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId;
      model.LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId;
      model.LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId;
      model.RoleFeatures = userAssociationMessage.RoleFeatures;
      model.InstituteBanner = userAssociationMessage.InstituteBanner;
      return View(model);
    }


    public JsonResult GetTransactionWithFilteredDate(long branchMediumId, string startDate, string endDate)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowTransactionListRequestMessage();
      requestMessage.BranchMediumId = branchMediumId;
      requestMessage.StartDate = Convert.ToDateTime(startDate);
      requestMessage.EndDate = Convert.ToDateTime(endDate);

      var response = _showTransactionListRequestHandler.Handle(requestMessage, cancellationToken);
      return Json(_showTransactionListResponsePresenter.Handle(response.Result, new List<TransactionViewModel>()));
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Accounting, (int)Feature.AccountingEnum.COAView)]
    public ActionResult ShowChartOfAccount(long branchMediumId)
    {
      var cancellationToken = new CancellationToken();
      var currentSession = _currentSessionProvider.GetCurrentSession(branchMediumId);
      var model = new AddAccoundHeadChildViewModel();

      if (currentSession != null)
      {
        var branchCaHeadsRequestMessage = new ShowBranchMediumAccountsHeadListRequestMessage
        {
          BranchMediumId = branchMediumId,
          CurrentSessionId = currentSession.Id
        };

        var branchCaHeadsResponse = _showBranchMediumAccountsHeadListRequestHandler.Handle(branchCaHeadsRequestMessage, cancellationToken);

        if (branchCaHeadsResponse.Result.BranchMediumAccountsHeadList.Count > 0)
        {
          var treeUi =
              _showBranchMediumAccountsHeadListResponsePresenter.Handle(branchCaHeadsResponse.Result);
          model = new AddAccoundHeadChildViewModel()
          {
            BranchMediumId = branchMediumId, 
            SessionId = branchCaHeadsRequestMessage.CurrentSessionId,
            TreeUi = treeUi,
            Sessions = new List<AddAccoundHeadChildViewModel.Session>()
          };

          var requestMessage = new DisplayBranchMediumRequestMessage { BranchMediumId = branchMediumId, LoadSessions = true };
          var response = _displayBranchMediumRequestHandler.Handle(requestMessage, cancellationToken);
          foreach (var resultSession in response.Result.Sessions)
          {
            model.Sessions.Add(new AddAccoundHeadChildViewModel.Session()
            {
              SessionId = resultSession.SessionId,
              SessionName = resultSession.SessionName
            });
          }
        }
        else
        {
          model = null;
        }

      }

      if (model == null)
        return new EmptyResult();

      return PartialView("AddAccountHead", model);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Accounting, (int)Feature.AccountingEnum.COAView)]
    public JsonResult ShowChartOfAccountFiltered(long branchMediumId,long sessionId)
    {
      var cancellationToken = new CancellationToken();
      var addChildAccountHeadModel = new AddAccoundHeadChildViewModel();
      var treeUi = string.Empty;
        var branchCaHeadsRequestMessage = new ShowBranchMediumAccountsHeadListRequestMessage
        {
          BranchMediumId = branchMediumId,
          CurrentSessionId = sessionId
        };

        var branchCaHeadsResponse = _showBranchMediumAccountsHeadListRequestHandler.Handle(branchCaHeadsRequestMessage, cancellationToken);
        var sessionRequestMessage = new ViewSessionRequestMessage { SessionId = sessionId, InstituteId = 0 };

        var sessionResponse = _showSessionRequestHandler.Handle(sessionRequestMessage, cancellationToken);
        addChildAccountHeadModel.IsSessionClosed = sessionResponse.Result.Session.IsSessionClosed;
      if (branchCaHeadsResponse.Result.BranchMediumAccountsHeadList.Count > 0)
        {
          addChildAccountHeadModel.TreeUi =
            _showBranchMediumAccountsHeadListResponsePresenter.Handle(branchCaHeadsResponse.Result);
          
        }
      return Json(addChildAccountHeadModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.FinancialStatements, (int)Feature.FinancialStatementEnum.TrialBalance)]
    public ActionResult ShowTrialBalanceDefault(long branchMediumId)
    {
      var cancellationToken = new CancellationToken();
      var currentSession = _currentSessionProvider.GetCurrentSession(branchMediumId);
      var requestMessage = new DisplayBranchMediumRequestMessage { BranchMediumId = branchMediumId, LoadSessions = true };
      var response = _displayBranchMediumRequestHandler.Handle(requestMessage, cancellationToken);
      var accountDisplayModel = _accountDisplayResponsePresenter.Handle(response.Result, new AccountDisplayModel(), GetUserAssociationResponseMessage());
      var trialBalanceViewModel = new TrialBalanceViewModel();

      if (currentSession != null)
      {
        var showTrialBalanceRequestMessage = new ShowTrialBalanceRequestMessage
        {
          BranchMediumId = branchMediumId,
          StartDate = currentSession.StartDate,
          EndDate = DateTime.Now,
          CurrentUserName = HttpContext.User.Identity.Name
        };

        var showTrialBalanceResponse = _showTrialBalanceRequestHandler.Handle(showTrialBalanceRequestMessage, cancellationToken);

        if (showTrialBalanceResponse.Result.Data != null)
        {
          trialBalanceViewModel = _showTrialBalanceTransactionResponsePresenter.Handle(showTrialBalanceResponse.Result.Data, trialBalanceViewModel);
          TrialBalanceViewModel.BranchMedium trialBalanceBranchMedium = new TrialBalanceViewModel.BranchMedium()
          {
            BranchMediumId = accountDisplayModel.AccountDisplayBranchMedium.BranchMediumId,
            BranchMediumName = ""
          };
          trialBalanceViewModel.Branch = trialBalanceBranchMedium;
          trialBalanceViewModel.StartDate = currentSession.StartDate;
          trialBalanceViewModel.EndDate = DateTime.Now;
          accountDisplayModel.TrialBalanceViewModel = trialBalanceViewModel;
          accountDisplayModel.CurrentSession = new AccountDisplayModel.Session()
          {
            StartDate = currentSession.StartDate,
            EndDate = currentSession.EndDate,
            SessionId = currentSession.Id,
            SessionName = currentSession.SessionName
          };
        }
        else
        {
          trialBalanceViewModel = null;
        }

      }

      if (trialBalanceViewModel == null)
        return new EmptyResult();

      return PartialView("ShowTrialBalance", accountDisplayModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.FinancialStatements, (int)Feature.FinancialStatementEnum.IncomeStatement)]
    public ActionResult ShowIncomeStatementDefault(long branchMediumId)
    {
      var cancellationToken = new CancellationToken();
      var currentSession = _currentSessionProvider.GetCurrentSession(branchMediumId);
      var requestMessage = new DisplayBranchMediumRequestMessage { BranchMediumId = branchMediumId, LoadSessions = true };
      var response = _displayBranchMediumRequestHandler.Handle(requestMessage, cancellationToken);
      var accountDisplayModel = _accountDisplayResponsePresenter.Handle(response.Result, new AccountDisplayModel(), GetUserAssociationResponseMessage());
      var incomeStatementViewModel = new IncomeStatementViewModel();

      if (currentSession != null)
      {
        var showIncomeStatementRequestMessage = new ShowIncomeStatementRequestMessage
        {
          BranchMediumId = branchMediumId,
          StartDate = currentSession.StartDate,
          EndDate = DateTime.Now,
          CurrentUserName = HttpContext.User.Identity.Name
        };

        var showIncomeStatementResponse = _showIncomeStatementRequestHandler.Handle(showIncomeStatementRequestMessage, cancellationToken);

        if (showIncomeStatementResponse.Result.Data != null)
        {
          incomeStatementViewModel = _showIncomeStatementTransactionResponsePresenter.Handle(showIncomeStatementResponse.Result.Data, incomeStatementViewModel);
          IncomeStatementViewModel.BranchMedium incomeStatementBranchMedium = new IncomeStatementViewModel.BranchMedium()
          {
            BranchMediumId = accountDisplayModel.AccountDisplayBranchMedium.BranchMediumId,
            BranchMediumName = ""
          };
          incomeStatementViewModel.Branch = incomeStatementBranchMedium;
          incomeStatementViewModel.StartDate = currentSession.StartDate;
          incomeStatementViewModel.EndDate = DateTime.Now;
          accountDisplayModel.IncomeStatementViewModel = incomeStatementViewModel;
          accountDisplayModel.CurrentSession = new AccountDisplayModel.Session()
          {
            StartDate = currentSession.StartDate,
            EndDate = currentSession.EndDate,
            SessionId = currentSession.Id,
            SessionName = currentSession.SessionName
          };
        }
        else
        {
          incomeStatementViewModel = null;
        }

      }

      if (incomeStatementViewModel == null)
        return new EmptyResult();

      return PartialView("ShowIncomeStatement", accountDisplayModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.FinancialStatements, (int)Feature.FinancialStatementEnum.BalanceSheet)]
    public ActionResult ShowBalanceSheetDefault(long branchMediumId)
    {
      var cancellationToken = new CancellationToken();
      var currentSession = _currentSessionProvider.GetCurrentSession(branchMediumId);
      var requestMessage = new DisplayBranchMediumRequestMessage { BranchMediumId = branchMediumId, LoadSessions = true };
      var response = _displayBranchMediumRequestHandler.Handle(requestMessage, cancellationToken);
      var accountDisplayModel = _accountDisplayResponsePresenter.Handle(response.Result, new AccountDisplayModel(), GetUserAssociationResponseMessage());
      var balanceSheetViewModel = new BalanceSheetViewModel();

      if (currentSession != null)
      {
        var showBalanceSheetRequestMessage = new ShowBalanceSheetRequestMessage
        {
          BranchMediumId = branchMediumId,
          StartDate = currentSession.StartDate,
          EndDate = DateTime.Now,
        };

        var showBalanceSheetResponse = _showBalanceSheetRequestHandler.Handle(showBalanceSheetRequestMessage, cancellationToken);

        if (showBalanceSheetResponse.Result.Data != null)
        {
          balanceSheetViewModel = _showBalanceSheetTransactionResponsePresenter.Handle(showBalanceSheetResponse.Result.Data, balanceSheetViewModel);
          BalanceSheetViewModel.BranchMedium balanceSheetBranchMedium = new BalanceSheetViewModel.BranchMedium()
          {
            BranchMediumId = accountDisplayModel.AccountDisplayBranchMedium.BranchMediumId,
            BranchMediumName = ""
          };
          balanceSheetViewModel.Branch = balanceSheetBranchMedium;
          balanceSheetViewModel.StartDate = currentSession.StartDate;
          balanceSheetViewModel.EndDate = DateTime.Now;
          accountDisplayModel.BalanceSheetViewModel = balanceSheetViewModel;
          accountDisplayModel.CurrentSession = new AccountDisplayModel.Session()
          {
            StartDate = currentSession.StartDate,
            EndDate = currentSession.EndDate,
            SessionId = currentSession.Id,
            SessionName = currentSession.SessionName
          };
        }
        else
        {
          balanceSheetViewModel = null;
        }

      }

      if (balanceSheetViewModel == null)
        return new EmptyResult();

      return PartialView("ShowBalanceSheet", accountDisplayModel);
    }
  }
}