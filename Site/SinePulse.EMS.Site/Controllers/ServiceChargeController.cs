using System.Collections.Generic;
using System.Threading;
using Microsoft.AspNetCore.Mvc;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.ServiceChargeMessages;
using SinePulse.EMS.Messages.BranchMessages;
using SinePulse.EMS.Messages.InstituteMessages;
using SinePulse.EMS.Messages.MediumMessages;
using SinePulse.EMS.Site.Models;
using SinePulse.EMS.Site.Presenters;
using SinePulse.EMS.UseCases.Authorization;
using SinePulse.EMS.UseCases.Branches;
using SinePulse.EMS.UseCases.Institutes;
using SinePulse.EMS.UseCases.Mediums;
using SinePulse.EMS.UseCases.ServiceCharges;
using Microsoft.AspNetCore.Authorization;
using SinePulse.EMS.Site.Authorization;
using SinePulse.EMS.Domain.Features;

namespace SinePulse.EMS.Site.Controllers
{
  [Authorize]
  public class ServiceChargeController : BaseController
  {
    private readonly ShowInstituteListRequestHandler _showInstituteListRequestHandler;
    private readonly ShowMediumListRequestHandler _showMediumListRequestHandler;
    private readonly ShowBranchListRequestHandler _showBranchListRequestHandler;
    private readonly AddServiceChargeRequestHandler _addServiceChargeRequestHandler;
    private readonly AddServiceChargeResponsePresenter _addServiceChargeResponsePresenter;
    private readonly GetServiceChargeRequestHandler _getServiceChargeRequestHandler;
    private readonly ShowServiceChargeListRequestHandler _showServiceChargeListRequestHandler;
    private readonly ShowServiceChargeListResponsePresenter _showServiceChargeListResponsePresenter;
    private readonly DisplayEditServiceChargeRequestHandler _displayEditServiceChargeRequestHandler; 
    private readonly DisplayEditServiceChargeResponsePresenter _displayEditServiceChargeResponsePresenter;
    private readonly EditServiceChargeRequestHandler _editServiceChargeRequestHandler;
    private readonly EditServiceChargeResponsePresenter _editServiceChargeResponsePresenter;
    public ServiceChargeController(ShowInstituteListRequestHandler showInstituteListRequestHandler,
      ShowMediumListRequestHandler showMediumListRequestHandler,
      ShowBranchListRequestHandler showBranchListRequestHandler,
      AddServiceChargeRequestHandler addServiceChargeRequestHandler,
      AddServiceChargeResponsePresenter addServiceChargeResponsePresenter,
      GetServiceChargeRequestHandler getServiceChargeRequestHandler,
      ShowServiceChargeListRequestHandler showServiceChargeListRequestHandler,
      ShowServiceChargeListResponsePresenter showServiceChargeListResponsePresenter,
      DisplayEditServiceChargeRequestHandler displayEditServiceChargeRequestHandler,
      DisplayEditServiceChargeResponsePresenter displayEditServiceChargeResponsePresenter,
      EditServiceChargeRequestHandler editServiceChargeRequestHandler,
      EditServiceChargeResponsePresenter editServiceChargeResponsePresenter,
    GetUserAssociationRequestHandler getUserAssociationRequestHandler) : base(getUserAssociationRequestHandler)
    {
      _showInstituteListRequestHandler = showInstituteListRequestHandler;
      _showMediumListRequestHandler = showMediumListRequestHandler;
      _showBranchListRequestHandler = showBranchListRequestHandler;
      _addServiceChargeRequestHandler = addServiceChargeRequestHandler;
      _addServiceChargeResponsePresenter = addServiceChargeResponsePresenter;
      _getServiceChargeRequestHandler = getServiceChargeRequestHandler;
      _showServiceChargeListRequestHandler = showServiceChargeListRequestHandler;
      _showServiceChargeListResponsePresenter = showServiceChargeListResponsePresenter;
      _displayEditServiceChargeResponsePresenter = displayEditServiceChargeResponsePresenter;
      _displayEditServiceChargeRequestHandler = displayEditServiceChargeRequestHandler;
      _editServiceChargeRequestHandler = editServiceChargeRequestHandler;
      _editServiceChargeResponsePresenter = editServiceChargeResponsePresenter;
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.SuperAdminServiceType, (int)Feature.SuperAdminServiceChargeEnum.AddServiceCharge)]
    [HttpGet]
    public ActionResult AddServiceCharge()
    {
      GetUserAssociationResponseMessage userAssociationMessage = GetUserAssociationResponseMessage();
      var viewModel = new AddServiceChargeViewModel();
      viewModel.Institutes = GetInstitutes();
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

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.SuperAdminServiceType, (int)Feature.SuperAdminServiceChargeEnum.AddServiceCharge)]
    [HttpPost]
    public ActionResult AddServiceCharge(AddServiceChargeViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new AddServiceChargeRequestMessage();

      requestMessage.BranchMediumId = model.BranchMediumId;
      requestMessage.EffectiveDate = model.EffectiveDate;
      requestMessage.PerStudentCharge = model.PerStudentCharge;
      requestMessage.PaymentBufferPeriod = model.PaymentBufferPeriod;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;

      var response = _addServiceChargeRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _addServiceChargeResponsePresenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());

      if (response.Result.ValidationResult.IsValid)
        return RedirectToAction("Index");
      viewModel.Institutes = GetInstitutes();
      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.SuperAdminServiceType, (int)Feature.SuperAdminServiceChargeEnum.ViewServiceCharge)]
    public ActionResult Index()
    {
      GetUserAssociationResponseMessage userAssociationMessage = GetUserAssociationResponseMessage();
      var viewModel = new AddServiceChargeViewModel();
      viewModel.Institutes = GetInstitutes();
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

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.SuperAdminServiceType, (int)Feature.SuperAdminServiceChargeEnum.ViewServiceCharge)]
    public ActionResult ShowServiceCharge(long branchMediumId)
    {
      if (branchMediumId > 0)
      {

        var cancellationToken = new CancellationToken();
        var requestMessage = new ShowServiceChargeListRequestMessage()
        {
          BranchMediumId = branchMediumId
        };
        var response = _showServiceChargeListRequestHandler.Handle(requestMessage, cancellationToken);
        var viewModel = _showServiceChargeListResponsePresenter.Handle(response.Result.Data, new ShowServiceChargeListViewModel(),
          GetUserAssociationResponseMessage());
        return PartialView("ShowServiceChargeList", viewModel);
      }
      return View();
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.SuperAdminServiceType, (int)Feature.SuperAdminServiceChargeEnum.EditServiceCharge)]
    [HttpGet]
    public ActionResult EditServiceCharge(long serviceChargeId, long branchMediumId)
    {
        var cancellationToken = new CancellationToken();
        var requestMessage = new DisplayEditServiceChargeRequestMessage()
        {
          ServiceChargeId = serviceChargeId,
          BranchMediumId = branchMediumId
        };
      var model = new EditServiceChargeViewModel
      {
        ServiceChargeId = serviceChargeId,
        BranchMediumId = branchMediumId
      };

      var response = _displayEditServiceChargeRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _displayEditServiceChargeResponsePresenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());
      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.SuperAdminServiceType, (int)Feature.SuperAdminServiceChargeEnum.EditServiceCharge)]
    [HttpPost]
    public ActionResult EditServiceCharge(EditServiceChargeViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new EditServiceChargeRequestMessage();

      requestMessage.ServiceChargeId = model.ServiceChargeId;
      requestMessage.BranchMediumId = model.BranchMediumId;
      requestMessage.PerStudentCharge = model.PerStudentCharge;
      requestMessage.PaymentBufferPeriod = model.PaymentBufferPeriod;
      requestMessage.EffectiveDate = model.EffectiveDate;
      requestMessage.EndDate = model.EndDate;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;

      var response = _editServiceChargeRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _editServiceChargeResponsePresenter.Handle(response.Result, model, ModelState,
        GetUserAssociationResponseMessage());
      if (!response.Result.ValidationResult.IsValid)
      {
        return View(viewModel);
      }
      return RedirectToAction("Index");
    }

    private IEnumerable<Institute> GetInstitutes()
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowInstituteListRequestMessage();
      var response = _showInstituteListRequestHandler.Handle(requestMessage, cancellationToken);
      return response.Result.InstituteList;
    }

    [HttpPost]
    public JsonResult GetBranches(long instituteId)
    {
      if (instituteId > 0)
      {
        var cancellationToken = new CancellationToken();
        var requestMessage = new ShowBranchListRequestMessage();
        requestMessage.InstituteId = instituteId;
        var response = _showBranchListRequestHandler.Handle(requestMessage, cancellationToken);
        return Json(response.Result.Branches);
      }
      return Json(null);
    }

    [HttpPost]
    public JsonResult GetMediums(long branchId)
    {
      if (branchId > 0)
      {
        var cancellationToken = new CancellationToken();
        var requestMessage = new ShowMediumListRequestMessage();
        requestMessage.BranchId = branchId;
        var response = _showMediumListRequestHandler.Handle(requestMessage, cancellationToken);
        return Json(response.Result.Mediums);
      }
      return Json(null);
    }
  }
}