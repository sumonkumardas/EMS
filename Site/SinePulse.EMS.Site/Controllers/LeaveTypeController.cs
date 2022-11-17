using System.Collections.Generic;
using System.Threading;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Features;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.LeaveTypeMessages;
using SinePulse.EMS.Site.Authorization;
using SinePulse.EMS.Site.Models;
using SinePulse.EMS.Site.Presenters;
using SinePulse.EMS.UseCases.Authorization;
using SinePulse.EMS.UseCases.LeaveTypes;

namespace SinePulse.EMS.Site.Controllers
{
  [Authorize]
  public class LeaveTypeController : BaseController
  {
    private readonly AddLeaveTypeRequestHandler _addLeaveTypeRequestHandler;
    private readonly AddLeaveTypeResponsePresenter _addLeaveTypeResponsePresenter;
    private readonly ShowLeaveTypeRequestHandler _showLeaveTypeRequestHandler;
    private readonly ShowLeaveTypeResponsePresenter _showLeaveTypeResponsePresenter;
    private readonly ShowLeaveTypeListRequestHandler _showLeaveTypeListRequestHandler;
    private readonly ShowLeaveTypeListResponsePresenter _showLeaveTypeListResponsePresenter;
    private readonly EditLeaveTypeRequestHandler _editLeaveTypeRequestHandler;
    private readonly EditLeaveTypeResponsePresenter _editLeaveTypeResponsePresenter;
    public LeaveTypeController(AddLeaveTypeRequestHandler addLeaveTypeRequestHandler, AddLeaveTypeResponsePresenter addLeaveTypeResponsePresenter, ShowLeaveTypeRequestHandler showLeaveTypeRequestHandler, ShowLeaveTypeResponsePresenter showLeaveTypeResponsePresenter, ShowLeaveTypeListRequestHandler showLeaveTypeListRequestHandler, ShowLeaveTypeListResponsePresenter showLeaveTypeListResponsePresenter,
      EditLeaveTypeRequestHandler editLeaveTypeRequestHandler, EditLeaveTypeResponsePresenter editLeaveTypeResponsePresenter,
      GetUserAssociationRequestHandler getUserAssociationRequestHandler) : base(getUserAssociationRequestHandler)
    {
      _addLeaveTypeRequestHandler = addLeaveTypeRequestHandler;
      _addLeaveTypeResponsePresenter = addLeaveTypeResponsePresenter;
      _showLeaveTypeRequestHandler = showLeaveTypeRequestHandler;
      _showLeaveTypeResponsePresenter = showLeaveTypeResponsePresenter;
      _showLeaveTypeListRequestHandler = showLeaveTypeListRequestHandler;
      _showLeaveTypeListResponsePresenter = showLeaveTypeListResponsePresenter;
      _editLeaveTypeRequestHandler = editLeaveTypeRequestHandler;
      _editLeaveTypeResponsePresenter = editLeaveTypeResponsePresenter;
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.EmployeeSetup, (int)Feature.EmployeeSetupEnum.NewLeaveType)]
    [HttpGet]
    public ActionResult AddLeaveType()
    {
      GetUserAssociationResponseMessage userAssociationMessage = GetUserAssociationResponseMessage();
      var viewModel = new AddLeaveTypeViewModel();
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

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.EmployeeSetup, (int)Feature.EmployeeSetupEnum.NewLeaveType)]
    [HttpPost]
    public ActionResult AddLeaveType(AddLeaveTypeViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new AddLeaveTypeRequestMessage();

      requestMessage.LeaveName = model.LeaveName;
      requestMessage.LeavesPerYear = model.LeavesPerYear;
      requestMessage.PercentageOfLeaveCarriedForward = model.PercentageOfLeaveCarriedForward;
      requestMessage.WillLeaveCarriedForward = model.WillLeaveCarriedForward;
      requestMessage.CanEmployeesApplyBeyondTheCurrentLeaveBalance =
        model.CanEmployeesApplyBeyondTheCurrentLeaveBalance;
      requestMessage.Status = (Messages.Model.Enums.StatusEnum)StatusEnum.Active;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;

      var response = _addLeaveTypeRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _addLeaveTypeResponsePresenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());

      if (response.Result.LeaveTypeId > 0)
      {
        return RedirectToAction("Index");
      }
      else
        return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.EmployeeSetup, (int)Feature.EmployeeSetupEnum.ViewLeaveType)]
    public ActionResult Index()
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowLeaveTypeListRequestMessage();
      LeaveTypeListViewModel model = new LeaveTypeListViewModel();

      var response = _showLeaveTypeListRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _showLeaveTypeListResponsePresenter.Handle(response.Result, model, GetUserAssociationResponseMessage());

      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.EmployeeSetup, (int)Feature.EmployeeSetupEnum.EditLeaveType)]
    [HttpGet]
    public ActionResult UpdateLeaveType(long leaveTypeId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowLeaveTypeRequestMessage();
      requestMessage.LeaveTypeId = leaveTypeId;

      LeaveTypeViewModel model = new LeaveTypeViewModel();
      var response = _showLeaveTypeRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _showLeaveTypeResponsePresenter.Handle(response.Result, model, GetUserAssociationResponseMessage());

      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.EmployeeSetup, (int)Feature.EmployeeSetupEnum.EditLeaveType)]
    [HttpPost]
    public ActionResult UpdateLeaveType(LeaveTypeViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new EditLeaveTypeRequestMessage();

      requestMessage.Id = model.Id;
      requestMessage.LeaveName = model.LeaveName;
      requestMessage.LeavesPerYear = model.LeavesPerYear;
      requestMessage.PercentageOfLeaveCarriedForward = model.PercentageOfLeaveCarriedForward;
      requestMessage.WillLeaveCarriedForward = model.WillLeaveCarriedForward;
      requestMessage.CanEmployeesApplyBeyondTheCurrentLeaveBalance =
        model.CanEmployeesApplyBeyondTheCurrentLeaveBalance;
      requestMessage.Status = (Messages.Model.Enums.StatusEnum)StatusEnum.Active;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;

      var response = _editLeaveTypeRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _editLeaveTypeResponsePresenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());

      if (response.Result.ValidationResult.IsValid)
      {
        return RedirectToAction("Index");
      }
      else
        return View(viewModel);
    }
  }
}