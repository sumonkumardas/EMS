using System.Threading;
using Microsoft.AspNetCore.Mvc;
using SinePulse.EMS.Messages.BillingMessages;
using SinePulse.EMS.Site.Models;
using SinePulse.EMS.UseCases.Authorization;
using SinePulse.EMS.UseCases.Billings;

namespace SinePulse.EMS.Site.Controllers
{
  public class BillingController : BaseController
  {
    private readonly GetBillingDataRequestHandler _getBillingDataRequestHandler;
    private readonly GetBillingDataByYearRequestHandler _getBillingDataByYearRequestHandler;

    public BillingController(GetUserAssociationRequestHandler getUserAssociationRequestHandler,
      GetBillingDataRequestHandler getBillingDataRequestHandler,
      GetBillingDataByYearRequestHandler getBillingDataByYearRequestHandler) : base(getUserAssociationRequestHandler)
    {
      _getBillingDataRequestHandler = getBillingDataRequestHandler;
      _getBillingDataByYearRequestHandler = getBillingDataByYearRequestHandler;
    }

    public ActionResult LoadBillingInfoPartialView(long branchMediumId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new GetBillingDataRequestMessage();
      requestMessage.BranchMediumId = branchMediumId;
      var response = _getBillingDataRequestHandler.Handle(requestMessage, cancellationToken);
      return PartialView("~/Views/BranchMedium/BillingInfo.cshtml", new BillingInfoViewModel
      {
        BillingData = response.Result.Billings
      });
    }

    public JsonResult LoadBillingInfoByYear(long branchMediumId, int year)
    {
      if (year != 0)
      {
        var cancellationToken = new CancellationToken();
        var requestMessage = new GetBillingDataByYearRequestMessage {BranchMediumId = branchMediumId, Year = year};
        var response = _getBillingDataByYearRequestHandler.Handle(requestMessage, cancellationToken);
        return Json(response.Result.Billings);
      }

      return Json(new {invalidYearError = "Invalid Year!"});
    }
  }
}