using System.Threading;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SinePulse.EMS.Domain.Features;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.SalaryComponentTypeMessage;
using SinePulse.EMS.Site.Authorization;
using SinePulse.EMS.Site.Models;
using SinePulse.EMS.Site.Presenters;
using SinePulse.EMS.UseCases.Authorization;
using SinePulse.EMS.UseCases.SalaryComponentTypes;

namespace SinePulse.EMS.Site.Controllers
{
  [Authorize]
  public class SalaryComponentTypeController : BaseController
  {
    private readonly ShowSalaryComponentTypeListRequestHandler _showSalaryComponentTypeListRequestHandler;
    private readonly ShowSalaryComponentTypeListResponsePresenter _showSalaryComponentTypeListResponsePresenter;
    private readonly AddSalaryComponentTypeRequestHandler _addSalaryComponentTypeRequestHandler;
    private readonly AddSalaryComponentTypeResponsePresenter _addSalaryComponentTypeResponsePresenter;
    private readonly DisplayEditSalaryComponentTypeRequestHandler _displayEditSalaryComponentTypeRequestHandler;
    private readonly DisplayEditSalaryComponentTypeResponsePresenter _displayEditSalaryComponentTypeResponsePresenter;
    private readonly EditSalaryComponentTypeRequestHandler _editSalaryComponentTypeRequestHandler;
    private readonly EditSalaryComponentTypeResponsePresenter _editSalaryComponentTypeResponsePresenter;
    public SalaryComponentTypeController(
      ShowSalaryComponentTypeListRequestHandler showSalaryComponentTypeListRequestHandler,
      ShowSalaryComponentTypeListResponsePresenter showSalaryComponentTypeListResponsePresenter,
      AddSalaryComponentTypeRequestHandler addSalaryComponentTypeRequestHandler,
      AddSalaryComponentTypeResponsePresenter addSalaryComponentTypeResponsePresenter,
      DisplayEditSalaryComponentTypeRequestHandler displayEditSalaryComponentTypeRequestHandler,
      DisplayEditSalaryComponentTypeResponsePresenter displayEditSalaryComponentTypeResponsePresenter,
      EditSalaryComponentTypeRequestHandler editSalaryComponentTypeRequestHandler,
      EditSalaryComponentTypeResponsePresenter editSalaryComponentTypeResponsePresenter,
       GetUserAssociationRequestHandler getUserAssociationRequestHandler) : base(getUserAssociationRequestHandler)
    {
      _showSalaryComponentTypeListRequestHandler = showSalaryComponentTypeListRequestHandler;
      _showSalaryComponentTypeListResponsePresenter = showSalaryComponentTypeListResponsePresenter;
      _addSalaryComponentTypeRequestHandler = addSalaryComponentTypeRequestHandler;
      _addSalaryComponentTypeResponsePresenter = addSalaryComponentTypeResponsePresenter;
      _displayEditSalaryComponentTypeResponsePresenter = displayEditSalaryComponentTypeResponsePresenter;
      _displayEditSalaryComponentTypeRequestHandler = displayEditSalaryComponentTypeRequestHandler;
      _editSalaryComponentTypeRequestHandler = editSalaryComponentTypeRequestHandler;
      _editSalaryComponentTypeResponsePresenter = editSalaryComponentTypeResponsePresenter;
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.SuperAdminPayroll, (int)Feature.SuperAdminServiceChargeEnum.AddServiceCharge)]
    [HttpGet]
    public ActionResult AddSalaryComponentType()
    {
      GetUserAssociationResponseMessage userAssociationMessage = GetUserAssociationResponseMessage();

      var viewModel = new AddSalaryComponentTypeViewModel();
      viewModel.LoginName = userAssociationMessage.LoginName;
      viewModel.LoginImageUrl = userAssociationMessage.ImageUrl;
      viewModel.AssociatedWith = userAssociationMessage.AssociatedWith;
      viewModel.LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId;
      viewModel.LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId;
      viewModel.LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId;
      viewModel.RoleFeatures = userAssociationMessage.RoleFeatures;
      viewModel.InstituteBanner = userAssociationMessage.InstituteBanner;
      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.SuperAdminPayroll, (int)Feature.SuperAdminServiceChargeEnum.AddServiceCharge)]
    [HttpPost]
    public ActionResult AddSalaryComponentType(AddSalaryComponentTypeViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new AddSalaryComponentTypeRequestMessage();

      requestMessage.ComponentType = model.ComponentType;
      requestMessage.Status = model.Status;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;

      var response = _addSalaryComponentTypeRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _addSalaryComponentTypeResponsePresenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());

      if (response.Result.ValidationResult.IsValid)
        return RedirectToAction("Index");
      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.SuperAdminPayroll, (int)Feature.SuperAdminServiceChargeEnum.FindServiceCharge)]
    public IActionResult Index()
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowSalaryComponentTypeListRequestMessage();
      var response = _showSalaryComponentTypeListRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _showSalaryComponentTypeListResponsePresenter.Handle(response.Result.Data, new ShowSalaryComponentTypeListViewModel(),
        GetUserAssociationResponseMessage());
      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.SuperAdminPayroll, (int)Feature.SuperAdminServiceChargeEnum.EditServiceCharge)]
    [HttpGet]
    public ActionResult EditSalaryComponentType(long salaryComponentTypeId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new DisplayEditSalaryComponentTypeRequestMessage()
      {
        SalaryComponentTypeId = salaryComponentTypeId,
      };
      var model = new EditSalaryComponentTypeViewModel
      {
        SalaryComponentTypeId = salaryComponentTypeId,
      };

      var response = _displayEditSalaryComponentTypeRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _displayEditSalaryComponentTypeResponsePresenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());
      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.SuperAdminPayroll, (int)Feature.SuperAdminServiceChargeEnum.EditServiceCharge)]
    [HttpPost]
    public ActionResult EditSalaryComponentType(EditSalaryComponentTypeViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new EditSalaryComponentTypeRequestMessage();

      requestMessage.SalaryComponentTypeId = model.SalaryComponentTypeId;
      requestMessage.ComponentType = model.ComponentType;
      requestMessage.Status = model.Status;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;

      var response = _editSalaryComponentTypeRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _editSalaryComponentTypeResponsePresenter.Handle(response.Result, model, ModelState,
        GetUserAssociationResponseMessage());
      if (!response.Result.ValidationResult.IsValid)
      {
        return View(viewModel);
      }
      return RedirectToAction("Index");
    }
  }
}