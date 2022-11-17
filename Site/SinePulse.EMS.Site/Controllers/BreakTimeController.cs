using System.Threading;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Features;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.BreakTimeMessages;
using SinePulse.EMS.Site.Authorization;
using SinePulse.EMS.Site.Models;
using SinePulse.EMS.Site.Presenters;
using SinePulse.EMS.UseCases.Authorization;
using SinePulse.EMS.UseCases.BreakTimes;

namespace SinePulse.EMS.Site.Controllers
{
  [Authorize]
  public class BreakTimeController : BaseController
  {
    private readonly AddBreakTimeRequestHandler _addBreakTimeRequestHandler;
    private readonly AddBreakTimeResponsePresenter _presenter;
    private readonly GetClassBreakTimeRequestHandler _getClassBreakTimeRequestHandler;

    public BreakTimeController(AddBreakTimeRequestHandler addBreakTimeRequestHandler,
      AddBreakTimeResponsePresenter presenter,
      GetUserAssociationRequestHandler getUserAssociationRequestHandler, 
      GetClassBreakTimeRequestHandler getClassBreakTimeRequestHandler) : base(getUserAssociationRequestHandler)
    {
      _addBreakTimeRequestHandler = addBreakTimeRequestHandler;
      _presenter = presenter;
      _getClassBreakTimeRequestHandler = getClassBreakTimeRequestHandler;
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.BranchMedium, (int)Feature.BranchMediumEnum.AddClassBreakDown)]
    [HttpGet]
    public ActionResult AddBreakTime(long branchMediumId)
    {      
      var userAssociationMessage = GetUserAssociationResponseMessage();
      var viewModel = new AddBreakTimeViewModel
      {
        LoginName = userAssociationMessage.LoginName,
        LoginImageUrl = userAssociationMessage.ImageUrl,
        AssociatedWith = userAssociationMessage.AssociatedWith,
        LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId,
        LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId,
        LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId,
        RoleFeatures = userAssociationMessage.RoleFeatures,
        InstituteBanner = userAssociationMessage.InstituteBanner,
        BranchMediumId = branchMediumId
      };
      var cancellationToken = new CancellationToken();
      var requestMessage = new GetClassBreakTimeRequestMessage {BranchMediumId = branchMediumId};
      var response = _getClassBreakTimeRequestHandler.Handle(requestMessage, cancellationToken);
      viewModel.StartTime = response.Result.BreakTimeProperties.StartTime;
      viewModel.EndTime = response.Result.BreakTimeProperties.EndTime;
      return View(viewModel);

    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.BranchMedium, (int)Feature.BranchMediumEnum.AddClassBreakDown)]
    [HttpPost]
    public ActionResult AddBreakTime(AddBreakTimeViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new AddBreakTimeRequestMessage();

      requestMessage.StartTime = model.StartTime;
      requestMessage.EndTime = model.EndTime;
      requestMessage.BranchMediumId = model.BranchMediumId;
      requestMessage.Status = StatusEnum.Active;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;

      var response = _addBreakTimeRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _presenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());

      if (!response.Result.ValidationResult.IsValid)
      {
        viewModel.BranchMediumId = model.BranchMediumId;
        return View(viewModel);
      }

      return RedirectToAction("ViewBranchMedium", "BranchMedium", new { branchMediumId = model.BranchMediumId });
    }
  }
}