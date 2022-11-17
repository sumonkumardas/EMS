using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SinePulse.EMS.Domain.Features;
using SinePulse.EMS.Messages.EmployeeLeaveMessages;
using SinePulse.EMS.Messages.LeaveTypeMessages;
using SinePulse.EMS.Messages.Model.Enums;
using SinePulse.EMS.Site.Authorization;
using SinePulse.EMS.Site.Models;
using SinePulse.EMS.Site.Presenters;
using SinePulse.EMS.UseCases.Authorization;
using SinePulse.EMS.UseCases.EmployeeLeaves;
using SinePulse.EMS.UseCases.LeaveTypes;

namespace SinePulse.EMS.Site.Controllers
{
  [Authorize]
  public class EmployeeLeaveController : BaseController
  {
    private readonly AddEmployeeLeaveRequestHandler _addEmployeeLeaveRequestHandler;
    private readonly AddEmployeeLeaveResponsePresenter _addEmployeeLeaveResponsePresenter;
    private readonly ShowLeaveTypeListRequestHandler _showLeaveTypeListRequestHandler;
    private readonly ShowLeaveTypeListResponsePresenter _showLeaveTypeListResponsePresenter;
    private readonly ShowEmployeeLeaveListRequestHandler _showEmployeeLeaveListRequestHandler;
    private readonly ShowEmployeeLeaveListResponsePresenter _showEmployeeLeaveListResponsePresenter;
    private readonly ApproveLeaveRequestHandler _approveLeaveRequestHandler;

    public EmployeeLeaveController(AddEmployeeLeaveRequestHandler addEmployeeLeaveRequestHandler, AddEmployeeLeaveResponsePresenter addEmployeeLeaveResponsePresenter, ShowLeaveTypeListRequestHandler showLeaveTypeListRequestHandler, ShowLeaveTypeListResponsePresenter showLeaveTypeListResponsePresenter,
      ShowEmployeeLeaveListRequestHandler showEmployeeLeaveListRequestHandler,
      ShowEmployeeLeaveListResponsePresenter showEmployeeLeaveListResponsePresenter,
      ApproveLeaveRequestHandler approveLeaveRequestHandler, 
      GetUserAssociationRequestHandler getUserAssociationRequestHandler) : base(getUserAssociationRequestHandler)
    {
      _addEmployeeLeaveRequestHandler = addEmployeeLeaveRequestHandler;
      _addEmployeeLeaveResponsePresenter = addEmployeeLeaveResponsePresenter;
      _showLeaveTypeListRequestHandler = showLeaveTypeListRequestHandler;
      _showLeaveTypeListResponsePresenter = showLeaveTypeListResponsePresenter;
      _showEmployeeLeaveListRequestHandler = showEmployeeLeaveListRequestHandler;
      _showEmployeeLeaveListResponsePresenter = showEmployeeLeaveListResponsePresenter;
      _approveLeaveRequestHandler = approveLeaveRequestHandler;
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Employee, (int)Feature.EmployeeEnum.ApplyLeave)]
    [HttpGet]
    public ActionResult AddEmployeeLeave(long employeeId)
    {
      var cancellationToken = new CancellationToken();
      var viewModel = new AddEmployeeLeaveViewModel();
      viewModel.EmployeeId = employeeId;

      var showLeaveTypeListRequestMessage = new ShowLeaveTypeListRequestMessage();
      LeaveTypeListViewModel showLeaveTypeListViewListModel = new LeaveTypeListViewModel();
      var showLeaveTypeListResponse = _showLeaveTypeListRequestHandler.Handle(showLeaveTypeListRequestMessage, cancellationToken);
      var pickedLeaveTypeListViewModel = _showLeaveTypeListResponsePresenter.Handle(showLeaveTypeListResponse.Result, showLeaveTypeListViewListModel, GetUserAssociationResponseMessage());

      viewModel.LeaveTypes = pickedLeaveTypeListViewModel.LeaveTypes;
      viewModel.LoginName = pickedLeaveTypeListViewModel.LoginName;
      viewModel.LoginImageUrl = pickedLeaveTypeListViewModel.LoginImageUrl;
      viewModel.AssociatedWith = pickedLeaveTypeListViewModel.AssociatedWith;
      viewModel.LoginUsersInstituteId = pickedLeaveTypeListViewModel.LoginUsersInstituteId;
      viewModel.LoginUsersBranchId = pickedLeaveTypeListViewModel.LoginUsersBranchId;
      viewModel.LoginUsersBranchMediumId = pickedLeaveTypeListViewModel.LoginUsersBranchMediumId;
      viewModel.RoleFeatures = pickedLeaveTypeListViewModel.RoleFeatures;
      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Employee, (int)Feature.EmployeeEnum.ApplyLeave)]
    [HttpPost]
    public ActionResult AddEmployeeLeave(AddEmployeeLeaveViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new AddEmployeeLeaveRequestMessage();

      requestMessage.EndDate = model.EndDate;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;
      requestMessage.EmployeeId = model.EmployeeId;
      requestMessage.LeaveTypeId = model.LeaveTypeId;
      requestMessage.Reason = model.Reason;
      requestMessage.StartDate = model.StartDate;

      var response = _addEmployeeLeaveRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _addEmployeeLeaveResponsePresenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());
      if (!response.Result.ValidationResult.IsValid)
      {
        viewModel.EmployeeId = model.EmployeeId;
        viewModel.LeaveTypes = GetLeaveTypes().LeaveTypes;
        return View(viewModel);
      }
      return Redirect("/Employee/ViewEmployee?employeeId="+model.EmployeeId+"&#tab_leave");
    }

    private LeaveTypeListViewModel GetLeaveTypes()
    {
      var cancellationToken = new CancellationToken();
      var showLeaveTypeListRequestMessage = new ShowLeaveTypeListRequestMessage();
      LeaveTypeListViewModel showLeaveTypeListViewListModel = new LeaveTypeListViewModel();
      var showLeaveTypeListResponse = _showLeaveTypeListRequestHandler.Handle(showLeaveTypeListRequestMessage, cancellationToken);
      var leaveTypes = _showLeaveTypeListResponsePresenter.Handle(showLeaveTypeListResponse.Result, showLeaveTypeListViewListModel, GetUserAssociationResponseMessage());
      return leaveTypes;
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Employee, (int)Feature.EmployeeEnum.ApproveLeave)]
    [HttpGet]
    public ActionResult ApproveEmployeeLeave(long employeeId)
    {
      var cancellationToken = new CancellationToken();
      var model = new EmployeeLeaveViewModel();

      var showEmployeeLeaveListRequestMessage = new ShowEmployeeLeaveListRequestMessage();
      showEmployeeLeaveListRequestMessage.EmployeeId = employeeId;
      showEmployeeLeaveListRequestMessage.LeaveStatus = Domain.Enums.LeaveStatusEnum.Pending;
      showEmployeeLeaveListRequestMessage.Year = DateTime.Now.Year;
      EmployeeLeaveListViewModel showEmployeeLeaveListViewListModel = new EmployeeLeaveListViewModel();
      var showEmployeeLeaveListResponse = _showEmployeeLeaveListRequestHandler.Handle(showEmployeeLeaveListRequestMessage, cancellationToken);
      var pickedLeaveTypeListViewModel = _showEmployeeLeaveListResponsePresenter.Handle(showEmployeeLeaveListResponse.Result, showEmployeeLeaveListViewListModel, GetUserAssociationResponseMessage());
      
      return View(pickedLeaveTypeListViewModel);
    }

    [HttpPost]
    public JsonResult GetAllEmployeeHolidayOfYear(int year)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowEmployeeLeaveListRequestMessage();
      requestMessage.Year = year;
      requestMessage.LeaveStatus = Domain.Enums.LeaveStatusEnum.Approved;
      EmployeeLeaveListViewModel model = new EmployeeLeaveListViewModel();


      var response = _showEmployeeLeaveListRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _showEmployeeLeaveListResponsePresenter.Handle(response.Result, model, GetUserAssociationResponseMessage());

      List<EventViewModel> publicHolidayListModel = new List<EventViewModel>();
      foreach (var item in viewModel.EmployeeLeaves)
      {
        var holidayEvent = new EventViewModel();
        holidayEvent.start = item.StartDate;
        holidayEvent.end = item.EndDate;
        holidayEvent.title = item.Employee.FullName;
        holidayEvent.url = "/EmployeeLeave/ViewEmployeeLeave?employeeLeaveId=" + item.Id;
        publicHolidayListModel.Add(holidayEvent);
      }
      return Json(publicHolidayListModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Employee, (int)Feature.EmployeeEnum.ApproveLeave)]
    public JsonResult ApproveLeaveDone(long employeeLeaveId)
    {
      if (employeeLeaveId > 0)
      {
        var cancellationToken = new CancellationToken();
        var approveLeaveRequestMessage = new ApproveLeaveRequestMessage();
        approveLeaveRequestMessage.LeaveId = employeeLeaveId;
        approveLeaveRequestMessage.LeaveStatus = LeaveStatusEnum.Approved;
        approveLeaveRequestMessage.CurrentUserName = HttpContext.User.Identity.Name;
        var response = _approveLeaveRequestHandler.Handle(approveLeaveRequestMessage, cancellationToken);
        return Json(new {successMessage = "Leave Approved...!"});
      }

      return Json(new {invalidLeveId = "Invalid Leave Id : "+employeeLeaveId});

    }
    
    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Employee, (int)Feature.EmployeeEnum.ApproveLeave)]
    public JsonResult ApproveLeaveCancel(long employeeLeaveId)
    {
      if (employeeLeaveId > 0)
      {
      var cancellationToken = new CancellationToken();
      var approveLeaveRequestMessage = new ApproveLeaveRequestMessage();
      approveLeaveRequestMessage.LeaveId = employeeLeaveId;
      approveLeaveRequestMessage.LeaveStatus = LeaveStatusEnum.Cancelled;
      approveLeaveRequestMessage.CurrentUserName = HttpContext.User.Identity.Name;
      var response = _approveLeaveRequestHandler.Handle(approveLeaveRequestMessage, cancellationToken);
      return Json(new {successMessage = "Leave Canceled...!"});
      }
      return Json(new {invalidLeveId = "Invalid Leave Id : "+employeeLeaveId});
    }
  }
}