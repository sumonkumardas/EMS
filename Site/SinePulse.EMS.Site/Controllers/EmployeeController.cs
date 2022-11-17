using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Features;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Domain.UserManagement;
using SinePulse.EMS.Messages.AttendanceMessages;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.BranchMediumMessages;
using SinePulse.EMS.Messages.DesignationMessages;
using SinePulse.EMS.Messages.EmployeeAddressMessages;
using SinePulse.EMS.Messages.EmployeeEducationalQualificationMessages;
using SinePulse.EMS.Messages.EmployeeGradeMessages;
using SinePulse.EMS.Messages.EmployeeLeaveMessages;
using SinePulse.EMS.Messages.EmployeeMessages;
using SinePulse.EMS.Messages.EmployeePersonalInfoMessages;
using SinePulse.EMS.Messages.InstituteMessages;
using SinePulse.EMS.Messages.JobTypeMessages;
using SinePulse.EMS.Messages.Model.Leaves;
using SinePulse.EMS.Site.Authorization;
using SinePulse.EMS.Site.Models;
using SinePulse.EMS.Site.Presenters;
using SinePulse.EMS.UseCases.Attendances;
using SinePulse.EMS.UseCases.Authorization;
using SinePulse.EMS.UseCases.BranchMediums;
using SinePulse.EMS.UseCases.Designations;
using SinePulse.EMS.UseCases.Employee;
using SinePulse.EMS.UseCases.EmployeeAddresses;
using SinePulse.EMS.UseCases.EmployeeEducationalQualifications;
using SinePulse.EMS.UseCases.EmployeeGrade;
using SinePulse.EMS.UseCases.EmployeeLeaves;
using SinePulse.EMS.UseCases.EmployeePersonalInfo;
using SinePulse.EMS.UseCases.Institutes;
using SinePulse.EMS.UseCases.JobTypes;
using SinePulse.EMS.UseCases.LoginUsers;
using SinePulse.EMS.UseCases.Roles;
using SinePulse.EMS.UseCases.Sessions;

namespace SinePulse.EMS.Site.Controllers
{
  [Authorize]
  public class EmployeeController : BaseController
  {
    private readonly AddEmployeeRequestHandler _addEmployeeRequestHandler;
    private readonly AddEmployeeResponsePresenter _addEmployeeResponsePresenter;
    private readonly EditEmployeeRequestHandler _editEmployeeRequestHandler;
    private readonly EditEmployeeResponsePresenter _editEmployeeResponsePresenter;
    private readonly ShowEmployeeRequestHandler _showEmployeeRequestHandler;
    private readonly ShowEmployeeResponsePresenter _showEmployeeResponsePresenter;
    private readonly ShowBranchMediumListRequestHandler _showBranchMediumListRequestHandler;
    private readonly ShowBranchMediumListResponsePresenter _showBranchMediumListResponsePresenter;
    private readonly ShowEmployeeListRequestHandler _showEmployeeListRequestHandler;
    private readonly ShowEmployeeListResponsePresenter _showEmployeeListResponsePresenter;
    private readonly ShowEmployeeGradeListRequestHandler _showEmployeeGradeListRequestHandler;
    private readonly ShowEmployeeGradeListResponsePresenter _showEmployeeGradeListResponsePresenter;
    private readonly ShowJobTypeListRequestHandler _showJobTypeListRequestHandler;
    private readonly ShowJobTypeListResponsePresenter _showJobTypeListResponsePresenter;
    private readonly ShowDesignationListRequestHandler _showDesignationListRequestHandler;
    private readonly ShowDesignationListResponsePresenter _showDesignationListResponsePresenter;
    private readonly AddEmployeeImageRequestHandler _addEmployeeImageRequestHandler;
    private readonly AddEmployeeAddressRequestHandler _addEmployeeAddressRequestHandler;
    private readonly AddEmployeeAddressResponsePresenter _addEmployeeAddressResponsePresenter;
    private readonly AddEmployeeAttendanceRequestHandler _addEmployeeAttendanceRequestHandler;
    private readonly AddEmployeeAttendanceResponsePresenter _addEmployeeAttendanceResponsePresenter;
    private readonly ShowInstituteListRequestHandler _showInstituteListRequestHandler;
    private readonly ShowInstituteListResponsePresenter _showInstituteListResponsePresenter;
    private readonly ShowCurrentMonthAttendanceListRequestHandler _showCurrentMonthAttendanceListRequestHandler;
    private readonly ShowEmployeeAttendanceListResponsePresenter _showEmployeeAttendanceListResponsePresenter;
    private readonly EditEmployeeAttendanceRequestHandler _editEmployeeAttendanceRequestHandler;
    private readonly EditEmployeeAttendanceResponsePresenter _editEmployeeAttendanceResponsePresenter;
    private readonly ShowEmployeeAttendanceRequestHandler _showEmployeeAttendanceRequestHandler;
    private readonly ShowEmployeeAttendanceResponsePresenter _showEmployeeAttendanceResponsePresenter;
    private readonly IHostingEnvironment _appEnvironment;
    private readonly GetEmployeeLeavesRequestHandler _getEmployeeLeavesRequestHandler;

    private readonly GetEmployeeEducationalQualificationsRequestHandler
      _getEmployeeEducationalQualificationsRequestHandler;

    private readonly ShowEmployeeEducationalQualificationsResponsePresenter
      _showEmployeeEducationalQualificationsResponsePresenter;

    private readonly GetEmployeeAddressRequestHandler _getEmployeeAddressRequestHandler;
    private readonly ShowEmployeeAddressResponsePresenter _showEmployeeAddressResponsePresenter;
    private readonly GetEmployeePersonalInfoRequestHandler _getEmployeePersonalInfoRequestHandler;
    private readonly ShowEmployeePersonalInfoResponsePresenter _showEmployeePersonalInfoResponsePresenter;
    private readonly GetAttendanceListByDateRangeRequestHandler _getAttendanceListByDateRange;
    private readonly GetPendingLeavesRequestHandler _getPendingLeavesRequestHandler;


    public EmployeeController(AddEmployeeRequestHandler addEmployeeRequestHandler,
      AddEmployeeResponsePresenter addEmployeeResponsePresenter,
      EditEmployeeRequestHandler editEmployeeRequestHandler,
      EditEmployeeResponsePresenter editEmployeeResponsePresenter,
      ShowEmployeeRequestHandler showEmployeeRequestHandler,
      ShowEmployeeResponsePresenter showEmployeeResponsePresenter,
      ShowBranchMediumListRequestHandler showBranchMediumListRequestHandler,
      ShowBranchMediumListResponsePresenter showBranchMediumListResponsePresenter,
      ShowEmployeeListRequestHandler showEmployeeListRequestHandler,
      ShowEmployeeListResponsePresenter showEmployeeListResponsePresenter,
      ShowEmployeeGradeListRequestHandler showEmployeeGradeListRequestHandler,
      ShowEmployeeGradeListResponsePresenter showEmployeeGradeListResponsePresenter,
      ShowJobTypeListRequestHandler showJobTypeListRequestHandler,
      ShowJobTypeListResponsePresenter showJobTypeListResponsePresenter,
      ShowDesignationListRequestHandler showDesignationListRequestHandler,
      ShowDesignationListResponsePresenter showDesignationListResponsePresenter,
      AddEmployeeImageRequestHandler addEmployeeImageRequestHandler,
      ShowEmployeeLeaveListRequestHandler showEmployeeLeaveListRequestHandler,
      ShowEmployeeLeaveListResponsePresenter showEmployeeLeaveListResponsePresenter,
      AddEmployeeAddressRequestHandler addEmployeeAddressRequestHandler,
      AddEmployeeAddressResponsePresenter addEmployeeAddressResponsePresenter,
      AddEmployeeAttendanceRequestHandler addEmployeeAttendanceRequestHandler,
      AddEmployeeAttendanceResponsePresenter addEmployeeAttendanceResponsePresenter,
      ShowCurrentMonthAttendanceListRequestHandler showCurrentMonthAttendanceListRequestHandler,
      ShowInstituteListRequestHandler showInstituteListRequestHandler,
      ShowInstituteListResponsePresenter showInstituteListResponsePresenter,
      ShowEmployeeAttendanceListResponsePresenter showEmployeeAttendanceListResponsePresenter,
      EditEmployeeAttendanceRequestHandler editEmployeeAttendanceRequestHandler,
      EditEmployeeAttendanceResponsePresenter editEmployeeAttendanceResponsePresenter,
      ShowEmployeeAttendanceRequestHandler showEmployeeAttendanceRequestHandler,
      ShowEmployeeAttendanceResponsePresenter showEmployeeAttendanceResponsePresenter,
      IHostingEnvironment appEnvironment, GetEmployeeLeavesRequestHandler getEmployeeLeavesRequestHandler,
      GetUserAssociationRequestHandler getUserAssociationRequestHandler,
      GetEmployeeEducationalQualificationsRequestHandler getEmployeeEducationalQualificationsRequestHandler,
      ShowEmployeeEducationalQualificationsResponsePresenter showEmployeeEducationalQualificationsResponsePresenter,
      GetEmployeeAddressRequestHandler getEmployeeAddressRequestHandler,
      ShowEmployeeAddressResponsePresenter showEmployeeAddressResponsePresenter,
      GetBranchMediumSessionsRequestHandler getBranchMediumSessionsRequestHandler,
      GetEmployeePersonalInfoRequestHandler getEmployeePersonalInfoRequestHandler,
      ShowEmployeePersonalInfoResponsePresenter showEmployeePersonalInfoResponsePresenter,
      GetAttendanceListByDateRangeRequestHandler getAttendanceListByDateRange,
      GetPendingLeavesRequestHandler getPendingLeavesRequestHandler,
      AddLoginUserRequestHandler addLoginUserRequestHandler,
      AddLoginUserResponsePresenter addLoginUserResponsePresenter,
      UserManager<LoginUser> userManager, ChangeLoginUserPasswordRequestHandler changeLoginUserPasswordRequestHandler,
      ChangeLoginUserPasswordResponsePresenter changeLoginUserPasswordLoginUserResponsePresenter,
      ShowRoleListRequestHandler showRoleListRequestHandler,
      ShowRoleRequestHandler showRoleRequestHandler) : base(getUserAssociationRequestHandler)
    {
      _addEmployeeRequestHandler = addEmployeeRequestHandler;
      _addEmployeeResponsePresenter = addEmployeeResponsePresenter;
      _showEmployeeRequestHandler = showEmployeeRequestHandler;
      _showEmployeeResponsePresenter = showEmployeeResponsePresenter;
      _showBranchMediumListRequestHandler = showBranchMediumListRequestHandler;
      _showBranchMediumListResponsePresenter = showBranchMediumListResponsePresenter;
      _showEmployeeListRequestHandler = showEmployeeListRequestHandler;
      _showEmployeeListResponsePresenter = showEmployeeListResponsePresenter;
      _showEmployeeGradeListRequestHandler = showEmployeeGradeListRequestHandler;
      _showEmployeeGradeListResponsePresenter = showEmployeeGradeListResponsePresenter;
      _showJobTypeListRequestHandler = showJobTypeListRequestHandler;
      _showJobTypeListResponsePresenter = showJobTypeListResponsePresenter;
      _showDesignationListRequestHandler = showDesignationListRequestHandler;
      _showDesignationListResponsePresenter = showDesignationListResponsePresenter;
      _addEmployeeImageRequestHandler = addEmployeeImageRequestHandler;
      _addEmployeeAddressRequestHandler = addEmployeeAddressRequestHandler;
      _addEmployeeAddressResponsePresenter = addEmployeeAddressResponsePresenter;
      _addEmployeeAttendanceRequestHandler = addEmployeeAttendanceRequestHandler;
      _addEmployeeAttendanceResponsePresenter = addEmployeeAttendanceResponsePresenter;
      _showInstituteListRequestHandler = showInstituteListRequestHandler;
      _showInstituteListResponsePresenter = showInstituteListResponsePresenter;
      _showCurrentMonthAttendanceListRequestHandler = showCurrentMonthAttendanceListRequestHandler;
      _showEmployeeAttendanceListResponsePresenter = showEmployeeAttendanceListResponsePresenter;
      _editEmployeeAttendanceRequestHandler = editEmployeeAttendanceRequestHandler;
      _editEmployeeAttendanceResponsePresenter = editEmployeeAttendanceResponsePresenter;
      _showEmployeeAttendanceRequestHandler = showEmployeeAttendanceRequestHandler;
      _showEmployeeAttendanceResponsePresenter = showEmployeeAttendanceResponsePresenter;
      _editEmployeeRequestHandler = editEmployeeRequestHandler;
      _editEmployeeResponsePresenter = editEmployeeResponsePresenter;
      _appEnvironment = appEnvironment;
      _getEmployeeLeavesRequestHandler = getEmployeeLeavesRequestHandler;
      _getEmployeeEducationalQualificationsRequestHandler = getEmployeeEducationalQualificationsRequestHandler;
      _showEmployeeEducationalQualificationsResponsePresenter = showEmployeeEducationalQualificationsResponsePresenter;
      _getEmployeeAddressRequestHandler = getEmployeeAddressRequestHandler;
      _showEmployeeAddressResponsePresenter = showEmployeeAddressResponsePresenter;
      _getEmployeePersonalInfoRequestHandler = getEmployeePersonalInfoRequestHandler;
      _showEmployeePersonalInfoResponsePresenter = showEmployeePersonalInfoResponsePresenter;
      _getAttendanceListByDateRange = getAttendanceListByDateRange;
      _getPendingLeavesRequestHandler = getPendingLeavesRequestHandler;
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Employee, (int) Feature.EmployeeEnum.NewEmployee)]
    [HttpGet]
    public ActionResult AddEmployee(AssociationType associationType, long? objectId)
    {
      var cancellationToken = new CancellationToken();
      GetUserAssociationResponseMessage userAssociationMessage = GetUserAssociationResponseMessage();
      var model = new AddEmployeeViewModel
      {
        LoginName = userAssociationMessage.LoginName,
        AssociatedWith = userAssociationMessage.AssociatedWith,
        LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId,
        LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId,
        LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId,
        RoleFeatures = userAssociationMessage.RoleFeatures,
        InstituteBanner = userAssociationMessage.InstituteBanner
      };

      var showBranchMediumListRequestMessage = new ShowBranchMediumListRequestMessage();
      List<BranchMediumViewModel> showBranchViewListModel = new List<BranchMediumViewModel>();
      var showBranchMediumListResponse =
        _showBranchMediumListRequestHandler.Handle(showBranchMediumListRequestMessage, cancellationToken);
      // var pickedBranchMediumListViewModel = _showBranchMediumListResponsePresenter.Handle(showBranchMediumListResponse.Result, showBranchViewListModel);

      var showEmployeeListRequestMessage = new ShowEmployeeListRequestMessage();
      showEmployeeListRequestMessage.AssociationType = associationType;
      showEmployeeListRequestMessage.ObjectId = objectId != null ? objectId.Value : 0;
      EmployeeListViewModel showemployeeListViewListModel = new EmployeeListViewModel();
      var showEmployeeListResponse =
        _showEmployeeListRequestHandler.Handle(showEmployeeListRequestMessage, cancellationToken);
      var pickedEmployeeListViewModel = _showEmployeeListResponsePresenter.Handle(showEmployeeListResponse.Result,
        showemployeeListViewListModel, GetUserAssociationResponseMessage());

      var showJobTypeListRequestMessage = new ShowJobTypeListRequestMessage();
      JobTypeListViewModel showJobTypeListViewListModel = new JobTypeListViewModel();
      var showJobTypeListResponse =
        _showJobTypeListRequestHandler.Handle(showJobTypeListRequestMessage, cancellationToken);
      var pickedJobTypeListViewModel = _showJobTypeListResponsePresenter.Handle(showJobTypeListResponse.Result,
        showJobTypeListViewListModel, GetUserAssociationResponseMessage());

      var showEmployeeGradeListRequestMessage = new ShowEmployeeGradeListRequestMessage();
      GradeListViewModel showEmployeeGradeListViewListModel = new GradeListViewModel();
      var showEmployeeGradeListResponse =
        _showEmployeeGradeListRequestHandler.Handle(showEmployeeGradeListRequestMessage, cancellationToken);
      var pickedEmployeeGradeListViewModel = _showEmployeeGradeListResponsePresenter.Handle(
        showEmployeeGradeListResponse.Result, showEmployeeGradeListViewListModel, GetUserAssociationResponseMessage());

      var showDesignationListRequestMessage = new ShowDesignationListRequestMessage();
      DesignationListViewModel showDesignationListViewListModel = new DesignationListViewModel();
      var showDesignationListResponse =
        _showDesignationListRequestHandler.Handle(showDesignationListRequestMessage, cancellationToken);
      var pickedDesignationListViewModel = _showDesignationListResponsePresenter.Handle(
        showDesignationListResponse.Result, showDesignationListViewListModel, GetUserAssociationResponseMessage());

      // model.Branches = pickedBranchMediumListViewModel;
      model.Employees = pickedEmployeeListViewModel.EmployeeList;
      model.JobTypes = pickedJobTypeListViewModel.JobTypes;
      model.Designations = pickedDesignationListViewModel.Designations;
      model.Grades = pickedEmployeeGradeListViewModel.Grades;
      model.AssociationType = associationType;
      if (objectId != null) model.ObjectId = (long) objectId;
      return View(model);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Employee, (int) Feature.EmployeeEnum.NewEmployee)]
    [HttpPost]
    public ActionResult AddEmployee(AddEmployeeViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new AddEmployeeRequestMessage();
      requestMessage.BankAccountNo = model.BankAccountNo;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;
      requestMessage.DesignationId = model.DesignationId;
      requestMessage.DOB = model.DOB;
      requestMessage.EmailAddress = model.EmailAddress;
      requestMessage.EmployeeType = model.EmployeeType;
      requestMessage.FullName = model.FullName;
      requestMessage.GradeId = model.GradeId;
      requestMessage.JobTypeId = model.JobTypeId;
      requestMessage.JoiningDate = model.JoiningDate;
      requestMessage.MobileNo = model.MobileNo;
      requestMessage.NationalId = model.NationalId;
      requestMessage.ObjectId = model.ObjectId;
      requestMessage.AssociationType = model.AssociationType;
      requestMessage.ReportToId = model.ReportToId;

      var response = _addEmployeeRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel =
        _addEmployeeResponsePresenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());

      if (response.Result.EmployeeId > 0)
      {
        switch (model.AssociationType)
        {
          case AssociationType.Institute:
            return RedirectToAction("ViewInstitute", "Institute",
              new {instituteId = model.ObjectId.Value, activeTab = TabEnum.Employee});
          case AssociationType.Branch:
            return RedirectToAction("ViewBranch", "Branch",
              new {branchId = model.ObjectId.Value, activeTab = TabEnum.Employee});
          case AssociationType.BranchMedium:
            return RedirectToAction("ViewBranchMedium", "BranchMedium",
              new {branchMediumId = model.ObjectId.Value, activeTab = TabEnum.Employee});
          default:
            return RedirectToAction("Index");
        }
      }
      else
      {
        var showBranchMediumListRequestMessage = new ShowBranchMediumListRequestMessage();
        List<BranchMediumViewModel> showBranchViewListModel = new List<BranchMediumViewModel>();
        var showBranchMediumListResponse =
          _showBranchMediumListRequestHandler.Handle(showBranchMediumListRequestMessage, cancellationToken);
        var pickedBranchMediumListViewModel =
          _showBranchMediumListResponsePresenter.Handle(showBranchMediumListResponse.Result, showBranchViewListModel);

        var showEmployeeListRequestMessage = new ShowEmployeeListRequestMessage();
        showEmployeeListRequestMessage.AssociationType = model.AssociationType;
        EmployeeListViewModel showemployeeListViewListModel = new EmployeeListViewModel();
        var showEmployeeListResponse =
          _showEmployeeListRequestHandler.Handle(showEmployeeListRequestMessage, cancellationToken);
        var pickedEmployeeListViewModel = _showEmployeeListResponsePresenter.Handle(showEmployeeListResponse.Result,
          showemployeeListViewListModel, GetUserAssociationResponseMessage());

        var showJobTypeListRequestMessage = new ShowJobTypeListRequestMessage();
        JobTypeListViewModel showJobTypeListViewListModel = new JobTypeListViewModel();
        var showJobTypeListResponse =
          _showJobTypeListRequestHandler.Handle(showJobTypeListRequestMessage, cancellationToken);
        var pickedJobTypeListViewModel = _showJobTypeListResponsePresenter.Handle(showJobTypeListResponse.Result,
          showJobTypeListViewListModel, GetUserAssociationResponseMessage());

        var showEmployeeGradeListRequestMessage = new ShowEmployeeGradeListRequestMessage();
        GradeListViewModel showEmployeeGradeListViewListModel = new GradeListViewModel();
        var showEmployeeGradeListResponse =
          _showEmployeeGradeListRequestHandler.Handle(showEmployeeGradeListRequestMessage, cancellationToken);
        var pickedEmployeeGradeListViewModel = _showEmployeeGradeListResponsePresenter.Handle(
          showEmployeeGradeListResponse.Result, showEmployeeGradeListViewListModel,
          GetUserAssociationResponseMessage());

        var showDesignationListRequestMessage = new ShowDesignationListRequestMessage();
        DesignationListViewModel showDesignationListViewListModel = new DesignationListViewModel();
        var showDesignationListResponse =
          _showDesignationListRequestHandler.Handle(showDesignationListRequestMessage, cancellationToken);
        var pickedDesignationListViewModel = _showDesignationListResponsePresenter.Handle(
          showDesignationListResponse.Result, showDesignationListViewListModel, GetUserAssociationResponseMessage());

        viewModel.Branches = pickedBranchMediumListViewModel;
        viewModel.Employees = pickedEmployeeListViewModel.EmployeeList.ToList();
        viewModel.JobTypes = pickedJobTypeListViewModel.JobTypes;
        model.Designations = pickedDesignationListViewModel.Designations;
        viewModel.Grades = pickedEmployeeGradeListViewModel.Grades;
        return View(viewModel);
      }

    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Employee, (int) Feature.EmployeeEnum.ViewEmployee)]
    public ActionResult Index(AssociationType associationType, long objectId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowEmployeeListRequestMessage();
      requestMessage.AssociationType = AssociationType.Global;
      EmployeeListViewModel model = new EmployeeListViewModel();

      var response = _showEmployeeListRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel =
        _showEmployeeListResponsePresenter.Handle(response.Result, model, GetUserAssociationResponseMessage());


      switch (associationType)
      {
        case AssociationType.Institute:
          return RedirectToAction("ViewInstitute", "Institute",
            new {instituteId = objectId, activeTab = TabEnum.Employee});
        case AssociationType.Branch:
          return RedirectToAction("ViewBranch", "Branch", new {branchId = objectId, activeTab = TabEnum.Employee});
        case AssociationType.BranchMedium:
          return RedirectToAction("ViewBranchMedium", "BranchMedium",
            new {branchMediumId = objectId, activeTab = TabEnum.Employee});
        default:
          return View(viewModel);
      }
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Employee, (int) Feature.EmployeeEnum.ViewEmployee)]
    public ActionResult ViewEmployee(AssociationType associationType, long employeeId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowEmployeeRequestMessage();
      requestMessage.EmployeeId = employeeId;
      EmployeeViewModel model = new EmployeeViewModel();

      var response = _showEmployeeRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel =
        _showEmployeeResponsePresenter.Handle(response.Result, model, GetUserAssociationResponseMessage());
      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Employee, (int) Feature.EmployeeEnum.EditEmployee)]
    [HttpGet]
    public ActionResult UpdateEmployee(AssociationType associationType, long? objectId, long employeeId)
    {
      var cancellationToken = new CancellationToken();
      var model = new EditEmployeeViewModel();

      var showBranchMediumListRequestMessage = new ShowBranchMediumListRequestMessage();
      List<BranchMediumViewModel> showBranchViewListModel = new List<BranchMediumViewModel>();
      var showBranchMediumListResponse =
        _showBranchMediumListRequestHandler.Handle(showBranchMediumListRequestMessage, cancellationToken);
      var pickedBranchMediumListViewModel =
        _showBranchMediumListResponsePresenter.Handle(showBranchMediumListResponse.Result, showBranchViewListModel);

      var showEmployeeListRequestMessage = new ShowEmployeeListRequestMessage();
      showEmployeeListRequestMessage.AssociationType = associationType;
      showEmployeeListRequestMessage.ObjectId = objectId.Value;
      EmployeeListViewModel showemployeeListViewListModel = new EmployeeListViewModel();
      var showEmployeeListResponse =
        _showEmployeeListRequestHandler.Handle(showEmployeeListRequestMessage, cancellationToken);
      var pickedEmployeeListViewModel = _showEmployeeListResponsePresenter.Handle(showEmployeeListResponse.Result,
        showemployeeListViewListModel, GetUserAssociationResponseMessage());

      var showJobTypeListRequestMessage = new ShowJobTypeListRequestMessage();
      JobTypeListViewModel showJobTypeListViewListModel = new JobTypeListViewModel();
      var showJobTypeListResponse =
        _showJobTypeListRequestHandler.Handle(showJobTypeListRequestMessage, cancellationToken);
      var pickedJobTypeListViewModel = _showJobTypeListResponsePresenter.Handle(showJobTypeListResponse.Result,
        showJobTypeListViewListModel, GetUserAssociationResponseMessage());

      var showEmployeeGradeListRequestMessage = new ShowEmployeeGradeListRequestMessage();
      GradeListViewModel showEmployeeGradeListViewListModel = new GradeListViewModel();
      var showEmployeeGradeListResponse =
        _showEmployeeGradeListRequestHandler.Handle(showEmployeeGradeListRequestMessage, cancellationToken);
      var pickedEmployeeGradeListViewModel = _showEmployeeGradeListResponsePresenter.Handle(
        showEmployeeGradeListResponse.Result, showEmployeeGradeListViewListModel, GetUserAssociationResponseMessage());

      var showDesignationListRequestMessage = new ShowDesignationListRequestMessage();
      DesignationListViewModel showDesignationListViewListModel = new DesignationListViewModel();
      var showDesignationListResponse =
        _showDesignationListRequestHandler.Handle(showDesignationListRequestMessage, cancellationToken);
      var pickedDesignationListViewModel = _showDesignationListResponsePresenter.Handle(
        showDesignationListResponse.Result, showDesignationListViewListModel, GetUserAssociationResponseMessage());

      var requestMessage = new ShowEmployeeRequestMessage();
      requestMessage.EmployeeId = employeeId;
      var response = _showEmployeeRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _showEmployeeResponsePresenter.Handle(response.Result, new EmployeeViewModel(),
        GetUserAssociationResponseMessage());
      model.LoginName = viewModel.LoginName;
      model.AssociatedWith = viewModel.AssociatedWith;
      model.LoginUsersInstituteId = viewModel.LoginUsersInstituteId;
      model.LoginUsersBranchId = viewModel.LoginUsersBranchId;
      model.LoginUsersBranchMediumId = viewModel.LoginUsersBranchMediumId;
      model.RoleFeatures = viewModel.RoleFeatures;
      model.BankAccountNo = viewModel.BankAccountNo;
      if (viewModel.Designation != null) model.DesignationId = viewModel.Designation.Id;
      model.DOB = viewModel.DOB;
      model.EmailAddress = viewModel.EmailAddress;
      model.EmployeeType = (EmployeeTypeEnum) viewModel.EmployeeType;
      model.FullName = viewModel.FullName;
      if (viewModel.Grade != null) model.GradeId = viewModel.Grade.Id;
      if (viewModel.JobType != null) model.JobTypeId = viewModel.JobType.Id;
      model.JoiningDate = viewModel.JoiningDate;
      model.MobileNo = viewModel.MobileNo;
      model.NationalId = viewModel.NationalId;
      model.ObjectId = viewModel.ObjectId;
      model.AssociatedWith = viewModel.AssociatedWith;
      model.ReportToId = viewModel.ReportToId;

      model.Branches = pickedBranchMediumListViewModel;
      model.Employees = pickedEmployeeListViewModel.EmployeeList;
      model.JobTypes = pickedJobTypeListViewModel.JobTypes;
      model.Designations = pickedDesignationListViewModel.Designations;
      model.Grades = pickedEmployeeGradeListViewModel.Grades;
      model.AssociatedWith = associationType;
      if (objectId != null) model.ObjectId = (long) objectId;
      model.Id = employeeId;

      return View(model);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Employee, (int) Feature.EmployeeEnum.EditEmployee)]
    [HttpPost]
    public ActionResult UpdateEmployee(EditEmployeeViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new EditEmployeeRequestMessage();
      requestMessage.BankAccountNo = model.BankAccountNo;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;
      requestMessage.DesignationId = model.DesignationId;
      requestMessage.DOB = model.DOB;
      requestMessage.EmailAddress = model.EmailAddress;
      requestMessage.EmployeeType = model.EmployeeType;
      requestMessage.FullName = model.FullName;
      requestMessage.GradeId = model.GradeId;
      requestMessage.JobTypeId = model.JobTypeId;
      requestMessage.JoiningDate = model.JoiningDate;
      requestMessage.MobileNo = model.MobileNo;
      requestMessage.NationalId = model.NationalId;
      requestMessage.ObjectId = model.ObjectId;
      requestMessage.AssociationType = model.AssociatedWith;
      requestMessage.ReportToId = model.ReportToId;
      requestMessage.Id = model.Id;
      var response = _editEmployeeRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel =
        _editEmployeeResponsePresenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());

      if (response.Result.ValidationResult.IsValid)
      {
        switch (model.AssociatedWith)
        {
          case AssociationType.Institute:
            return RedirectToAction("ViewInstitute", "Institute",
              new { instituteId = model.ObjectId, activeTab = TabEnum.Employee });
          case AssociationType.Branch:
            return RedirectToAction("ViewBranch", "Branch", new { branchId = model.ObjectId, activeTab = TabEnum.Employee });
          case AssociationType.BranchMedium:
            return RedirectToAction("ViewBranchMedium", "BranchMedium",
              new { branchMediumId = model.ObjectId, activeTab = TabEnum.Employee });
          default:
            return RedirectToAction("Index", "Employee");
        }
      }
      else
      {
        var showBranchMediumListRequestMessage = new ShowBranchMediumListRequestMessage();
        List<BranchMediumViewModel> showBranchViewListModel = new List<BranchMediumViewModel>();
        var showBranchMediumListResponse =
          _showBranchMediumListRequestHandler.Handle(showBranchMediumListRequestMessage, cancellationToken);
        var pickedBranchMediumListViewModel =
          _showBranchMediumListResponsePresenter.Handle(showBranchMediumListResponse.Result, showBranchViewListModel);

        var showEmployeeListRequestMessage = new ShowEmployeeListRequestMessage();
        showEmployeeListRequestMessage.AssociationType = model.AssociatedWith;
        EmployeeListViewModel showemployeeListViewListModel = new EmployeeListViewModel();
        var showEmployeeListResponse =
          _showEmployeeListRequestHandler.Handle(showEmployeeListRequestMessage, cancellationToken);
        var pickedEmployeeListViewModel = _showEmployeeListResponsePresenter.Handle(showEmployeeListResponse.Result,
          showemployeeListViewListModel, GetUserAssociationResponseMessage());

        var showJobTypeListRequestMessage = new ShowJobTypeListRequestMessage();
        JobTypeListViewModel showJobTypeListViewListModel = new JobTypeListViewModel();
        var showJobTypeListResponse =
          _showJobTypeListRequestHandler.Handle(showJobTypeListRequestMessage, cancellationToken);
        var pickedJobTypeListViewModel = _showJobTypeListResponsePresenter.Handle(showJobTypeListResponse.Result,
          showJobTypeListViewListModel, GetUserAssociationResponseMessage());

        var showEmployeeGradeListRequestMessage = new ShowEmployeeGradeListRequestMessage();
        GradeListViewModel showEmployeeGradeListViewListModel = new GradeListViewModel();
        var showEmployeeGradeListResponse =
          _showEmployeeGradeListRequestHandler.Handle(showEmployeeGradeListRequestMessage, cancellationToken);
        var pickedEmployeeGradeListViewModel = _showEmployeeGradeListResponsePresenter.Handle(
          showEmployeeGradeListResponse.Result, showEmployeeGradeListViewListModel,
          GetUserAssociationResponseMessage());
        var showDesignationListRequestMessage = new ShowDesignationListRequestMessage();
        DesignationListViewModel showDesignationListViewListModel = new DesignationListViewModel();
        var showDesignationListResponse =
          _showDesignationListRequestHandler.Handle(showDesignationListRequestMessage, cancellationToken);
        var pickedDesignationListViewModel = _showDesignationListResponsePresenter.Handle(
          showDesignationListResponse.Result, showDesignationListViewListModel, GetUserAssociationResponseMessage());

        viewModel.Branches = pickedBranchMediumListViewModel;
        viewModel.Employees = pickedEmployeeListViewModel.EmployeeList;
        viewModel.JobTypes = pickedJobTypeListViewModel.JobTypes;
        model.Designations = pickedDesignationListViewModel.Designations;
        viewModel.Grades = pickedEmployeeGradeListViewModel.Grades;
        return View(viewModel);
      }

    }


    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Employee, (int) Feature.EmployeeEnum.NewEmployee)]
    public ActionResult EmployeeImageUpload(long employeeId)
    {
      GetUserAssociationResponseMessage userAssociationMessage = GetUserAssociationResponseMessage();
      var model = new EmployeeViewModel()
      {
        LoginName = userAssociationMessage.LoginName,
        AssociatedWith = userAssociationMessage.AssociatedWith,
        LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId,
        LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId,
        LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId,
        RoleFeatures = userAssociationMessage.RoleFeatures,
        InstituteBanner = userAssociationMessage.InstituteBanner
      };

      model.Id = employeeId;
      return View(model);

    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Employee, (int) Feature.EmployeeEnum.NewEmployee)]
    [HttpPost]
    public ActionResult EmployeeImageUpload(EmployeeViewModel model)
    {
      var files = HttpContext.Request.Form.Files;
      foreach (var image in files)
      {
        if (image != null && image.Length > 0)
        {
          var file = image;
          var uploads = Path.Combine(_appEnvironment.WebRootPath, "Uploads\\Employee\\");
          if (file.Length > 0)
          {
            var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);
            using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
            {
              file.CopyTo(fileStream);
              var cancellationToken = new CancellationToken();
              var requestMessage = new AddEmployeeImageRequestMessage();
              requestMessage.EmployeeId = model.Id;
              requestMessage.ImageUrl = fileName;
              requestMessage.CurrentUserName = HttpContext.User.Identity.Name;
              var response = _addEmployeeImageRequestHandler.Handle(requestMessage, cancellationToken);
              if (!string.IsNullOrEmpty(response.Result.PreviousImage))
              {
                var previousImageFile = Path.Combine(_appEnvironment.WebRootPath, "Uploads\\Employee\\") +
                                        response.Result.PreviousImage;
                if (System.IO.File.Exists(previousImageFile))
                {
                  System.IO.File.Delete(previousImageFile);
                }
              }
            }

          }
        }
      }

      return RedirectToAction("ViewEmployee", new {employeeId = model.Id});
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Employee, (int) Feature.EmployeeEnum.AddAddress)]
    [HttpGet]
    public ActionResult AddEmployeeAddress(long employeeId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new GetEmployeeAddressRequestMessage
      {
        EmployeeId = employeeId
      };
      var response = _getEmployeeAddressRequestHandler.Handle(requestMessage, cancellationToken);
      GetUserAssociationResponseMessage userAssociationMessage = GetUserAssociationResponseMessage();
      var model = new AddEmployeeAddressViewModel
      {
        LoginName = userAssociationMessage.LoginName,
        AssociatedWith = userAssociationMessage.AssociatedWith,
        LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId,
        LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId,
        LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId,
        RoleFeatures = userAssociationMessage.RoleFeatures,
        InstituteBanner = userAssociationMessage.InstituteBanner,
        PresentAddressCity = response.Result.Address.PresentCity,
        PresentAddressPostalCode = response.Result.Address.PresentPostalCode,
        PresentAddressStreet1 = response.Result.Address.PresentStreet1,
        PresentAddressStreet2 = response.Result.Address.PresentStreet2,
        PermanentAddressCity = response.Result.Address.PermanentCity,
        PermanentAddressPostalCode = response.Result.Address.PermanentPostalCode,
        PermanentAddressStreet1 = response.Result.Address.PermanentStreet1,
        PermanentAddressStreet2 = response.Result.Address.PermanentStreet2

      };

      model.EmployeeId = employeeId;

      return View(model);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Employee, (int) Feature.EmployeeEnum.AddAddress)]
    [HttpPost]
    public ActionResult AddEmployeeAddress(AddEmployeeAddressViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new AddEmployeeAddressRequestMessage();
      requestMessage.EmployeeId = model.EmployeeId;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;
      requestMessage.PermanentAddressCity = model.PermanentAddressCity;
      requestMessage.PermanentAddressPostalCode = model.PermanentAddressPostalCode;
      requestMessage.PermanentAddressStreet1 = model.PermanentAddressStreet1;
      requestMessage.PermanentAddressStreet2 = model.PermanentAddressStreet2;
      requestMessage.PresentAddressCity = model.PresentAddressCity;
      requestMessage.PresentAddressPostalCode = model.PresentAddressPostalCode;
      requestMessage.PresentAddressStreet1 = model.PresentAddressStreet1;
      requestMessage.PresentAddressStreet2 = model.PresentAddressStreet2;
      requestMessage.SameAsPermanentAddress = model.SameAsPermanentAddress;

      var response = _addEmployeeAddressRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel =
        _addEmployeeAddressResponsePresenter.Handle(response.Result, model, ModelState,
          GetUserAssociationResponseMessage());

      if (response.Result.ValidationResult.IsValid)
      {
        return Redirect("/Employee/ViewEmployee?employeeId=" + model.EmployeeId + "#tab_address");
      }

      viewModel.EmployeeId = model.EmployeeId;
      return View(viewModel);

    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Employee, (int) Feature.EmployeeEnum.AddAttendance)]
    public ActionResult AddEmployeeAttendance(long employeeId)
    {
      GetUserAssociationResponseMessage userAssociationMessage = GetUserAssociationResponseMessage();
      var model = new AddEmployeeAttendanceViewModel()
      {
        LoginName = userAssociationMessage.LoginName,
        AssociatedWith = userAssociationMessage.AssociatedWith,
        LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId,
        LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId,
        LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId,
        RoleFeatures = userAssociationMessage.RoleFeatures,
        InstituteBanner = userAssociationMessage.InstituteBanner
      };

      model.EmployeeId = employeeId;

      return View(model);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Employee, (int) Feature.EmployeeEnum.AddAttendance)]
    [HttpPost]
    public ActionResult AddEmployeeAttendance(AddEmployeeAttendanceViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new AddEmployeeAttendanceRequestMessage();

      requestMessage.InTime = model.InTime;
      requestMessage.OutTime = model.OutTime;
      requestMessage.AttendanceDate = model.AttendanceDate;
      requestMessage.EmployeeId = model.EmployeeId;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;

      var response = _addEmployeeAttendanceRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _addEmployeeAttendanceResponsePresenter.Handle(response.Result, model, ModelState,
        GetUserAssociationResponseMessage());
      if (!response.Result.ValidationResult.IsValid)
      {
        return View(viewModel);
      }

      return RedirectToAction("ViewEmployee", new {EmployeeId = model.EmployeeId});
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Employee, (int) Feature.EmployeeEnum.ViewAttendance)]
    public ActionResult ShowEmployeeAttendanceList()
    {
      var cancellationToken = new CancellationToken();
      var model = new EmployeeAttendanceListViewModel();

      var showInstituteListRequestMessage = new ShowInstituteListRequestMessage();
      ShowInstituteListViewModel showInstituteListModel = new ShowInstituteListViewModel();
      var showInstituteListResponse =
        _showInstituteListRequestHandler.Handle(showInstituteListRequestMessage, cancellationToken);
      var pickedInstituteListViewModel = _showInstituteListResponsePresenter.Handle(showInstituteListResponse.Result,
        showInstituteListModel, GetUserAssociationResponseMessage());
      model.LoginName = pickedInstituteListViewModel.LoginName;
      model.AssociatedWith = pickedInstituteListViewModel.AssociatedWith;
      model.LoginUsersInstituteId = pickedInstituteListViewModel.LoginUsersInstituteId;
      model.LoginUsersBranchId = pickedInstituteListViewModel.LoginUsersBranchId;
      model.LoginUsersBranchMediumId = pickedInstituteListViewModel.LoginUsersBranchMediumId;
      model.RoleFeatures = pickedInstituteListViewModel.RoleFeatures;
      model.InstituteList = pickedInstituteListViewModel.InstituteViewModelList;
      return View(model);
    }

    [HttpPost]
    public IEnumerable<EmployeeAttendanceViewModel> ShowEmployeeAttendanceList(
      [FromBody] EmployeeAttendanceListViewModel model)
    {
      var cancellationToken = new CancellationToken();

      var showEmployeeAttendanceListRequestMessage = new ShowCurrentMonthAttendanceListRequestMessage();
      showEmployeeAttendanceListRequestMessage.EmployeeId = model.EmployeeId;
      showEmployeeAttendanceListRequestMessage.AttendanceStartDate = model.AttendanceStartDate;
      showEmployeeAttendanceListRequestMessage.AttendanceEndDate = model.AttendanceEndDate;
      List<EmployeeAttendanceViewModel> showEmployeeAttendanceListModel = new List<EmployeeAttendanceViewModel>();
      var response =
        _showCurrentMonthAttendanceListRequestHandler.Handle(showEmployeeAttendanceListRequestMessage,
          cancellationToken);
      var pickedEmployeeAttendanceListViewModel =
        _showEmployeeAttendanceListResponsePresenter.Handle(response.Result, showEmployeeAttendanceListModel);

      if (pickedEmployeeAttendanceListViewModel == null)
        return new List<EmployeeAttendanceViewModel>();
      else
        return pickedEmployeeAttendanceListViewModel;
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Employee, (int) Feature.EmployeeEnum.EditAttendace)]
    [HttpGet]
    public ActionResult UpdateEmployeeAttendance(long attendanceId, long employeeId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowEmployeeAttendanceRequestMessage();
      var model = new EmployeeAttendanceViewModel();
      requestMessage.AttendanceId = attendanceId;

      var response = _showEmployeeAttendanceRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel =
        _showEmployeeAttendanceResponsePresenter.Handle(response.Result, model, GetUserAssociationResponseMessage());
      viewModel.EmployeeId = employeeId;
      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Employee, (int) Feature.EmployeeEnum.EditAttendace)]
    [HttpPost]
    public ActionResult UpdateEmployeeAttendance(EmployeeAttendanceViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new EditEmployeeAttendanceRequestMessage();

      requestMessage.Id = model.Id;
      requestMessage.AttendanceDate = model.AttendanceDate;
      requestMessage.EmployeeId = model.Employee.Id;
      requestMessage.InTime = model.InTime;
      requestMessage.OutTime = model.OutTime;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;

      var response = _editEmployeeAttendanceRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _editEmployeeAttendanceResponsePresenter.Handle(response.Result, model, ModelState,
        GetUserAssociationResponseMessage());

      if (response.Result.ValidationResult.IsValid)
      {
        return RedirectToAction("ViewEmployee", new {employeeId = model.EmployeeId});
      }

      viewModel.EmployeeId = model.EmployeeId;
      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Employee, (int) Feature.EmployeeEnum.ViewAttendance)]
    public ActionResult ViewEmployeeAttendance(long employeeAttendanceId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowEmployeeAttendanceRequestMessage();
      requestMessage.AttendanceId = employeeAttendanceId;
      EmployeeAttendanceViewModel model = new EmployeeAttendanceViewModel();

      var response = _showEmployeeAttendanceRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel =
        _showEmployeeAttendanceResponsePresenter.Handle(response.Result, model, GetUserAssociationResponseMessage());

      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Employee, (int) Feature.EmployeeEnum.NewEmployee)]
    [HttpGet]
    public ActionResult AddTeacher(AssociationType associationType, long objectId)
    {
      var cancellationToken = new CancellationToken();

      GetUserAssociationResponseMessage userAssociationMessage = GetUserAssociationResponseMessage();
      var model = new AddEmployeeViewModel()
      {
        LoginName = userAssociationMessage.LoginName,
        AssociatedWith = userAssociationMessage.AssociatedWith,
        LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId,
        LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId,
        LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId,
        RoleFeatures = userAssociationMessage.RoleFeatures,
        InstituteBanner = userAssociationMessage.InstituteBanner
      };

      var showEmployeeListRequestMessage = new ShowEmployeeListRequestMessage();
      var showEmployeeListViewListModel = new EmployeeListViewModel();
      showEmployeeListRequestMessage.ObjectId = objectId;
      showEmployeeListRequestMessage.AssociationType = associationType;
      var showEmployeeListResponse =
        _showEmployeeListRequestHandler.Handle(showEmployeeListRequestMessage, cancellationToken);
      var pickedEmployeeListViewModel = _showEmployeeListResponsePresenter.Handle(showEmployeeListResponse.Result,
        showEmployeeListViewListModel, GetUserAssociationResponseMessage());

      var showJobTypeListRequestMessage = new ShowJobTypeListRequestMessage();
      var showJobTypeListViewListModel = new JobTypeListViewModel();
      var showJobTypeListResponse =
        _showJobTypeListRequestHandler.Handle(showJobTypeListRequestMessage, cancellationToken);
      var pickedJobTypeListViewModel = _showJobTypeListResponsePresenter.Handle(showJobTypeListResponse.Result,
        showJobTypeListViewListModel, GetUserAssociationResponseMessage());

      var showDesignationListRequestMessage = new ShowDesignationListRequestMessage()
      {
        EmployeeType = EmployeeTypeEnum.Teacher
      };
      var showDesignationListViewListModel = new DesignationListViewModel();
      var showDesignationListResponse =
        _showDesignationListRequestHandler.Handle(showDesignationListRequestMessage, cancellationToken);
      var pickedDesignationListViewModel = _showDesignationListResponsePresenter.Handle(
        showDesignationListResponse.Result, showDesignationListViewListModel, GetUserAssociationResponseMessage());

      var showEmployeeGradeListRequestMessage = new ShowEmployeeGradeListRequestMessage();
      var showEmployeeGradeListViewListModel = new GradeListViewModel();
      var showEmployeeGradeListResponse =
        _showEmployeeGradeListRequestHandler.Handle(showEmployeeGradeListRequestMessage, cancellationToken);
      var pickedEmployeeGradeListViewModel = _showEmployeeGradeListResponsePresenter.Handle(
        showEmployeeGradeListResponse.Result, showEmployeeGradeListViewListModel, GetUserAssociationResponseMessage());

      model.ObjectId = objectId;
      model.AssociationType = associationType;
      model.Employees = pickedEmployeeListViewModel.EmployeeList;
      model.JobTypes = pickedJobTypeListViewModel.JobTypes;
      model.Designations = pickedDesignationListViewModel.Designations;
      model.Grades = pickedEmployeeGradeListViewModel.Grades;
      return View(model);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Employee, (int) Feature.EmployeeEnum.NewEmployee)]
    [HttpPost]
    public ActionResult AddTeacher(AddEmployeeViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new AddEmployeeRequestMessage();
      requestMessage.BankAccountNo = model.BankAccountNo;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;
      requestMessage.DesignationId = model.DesignationId;
      requestMessage.DOB = model.DOB;
      requestMessage.EmailAddress = model.EmailAddress;
      requestMessage.EmployeeType = EmployeeTypeEnum.Teacher;
      requestMessage.FullName = model.FullName;
      requestMessage.GradeId = model.GradeId;
      requestMessage.JobTypeId = model.JobTypeId;
      requestMessage.JoiningDate = model.JoiningDate;
      requestMessage.MobileNo = model.MobileNo;
      requestMessage.NationalId = model.NationalId;
      requestMessage.ObjectId = model.ObjectId;
      requestMessage.AssociationType = model.AssociationType;
      requestMessage.ReportToId = model.ReportToId;
      var response = _addEmployeeRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel =
        _addEmployeeResponsePresenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());

      if (response.Result.EmployeeId > 0)
      {
        return RedirectToAction("ViewBranchMedium", "BranchMedium",
              new { branchMediumId = model.ObjectId, activeTab = TabEnum.Employee });
      }
      else
      {
        var showBranchMediumListRequestMessage = new ShowBranchMediumListRequestMessage();
        List<BranchMediumViewModel> showBranchViewListModel = new List<BranchMediumViewModel>();
        var showBranchMediumListResponse =
          _showBranchMediumListRequestHandler.Handle(showBranchMediumListRequestMessage, cancellationToken);
        var pickedBranchMediumListViewModel =
          _showBranchMediumListResponsePresenter.Handle(showBranchMediumListResponse.Result, showBranchViewListModel);

        var showEmployeeListRequestMessage = new ShowEmployeeListRequestMessage();
        showEmployeeListRequestMessage.AssociationType = model.AssociatedWith;
        EmployeeListViewModel showemployeeListViewListModel = new EmployeeListViewModel();
        var showEmployeeListResponse =
          _showEmployeeListRequestHandler.Handle(showEmployeeListRequestMessage, cancellationToken);
        var pickedEmployeeListViewModel = _showEmployeeListResponsePresenter.Handle(showEmployeeListResponse.Result,
          showemployeeListViewListModel, GetUserAssociationResponseMessage());

        var showJobTypeListRequestMessage = new ShowJobTypeListRequestMessage();
        JobTypeListViewModel showJobTypeListViewListModel = new JobTypeListViewModel();
        var showJobTypeListResponse =
          _showJobTypeListRequestHandler.Handle(showJobTypeListRequestMessage, cancellationToken);
        var pickedJobTypeListViewModel = _showJobTypeListResponsePresenter.Handle(showJobTypeListResponse.Result,
          showJobTypeListViewListModel, GetUserAssociationResponseMessage());

        var showDesignationListRequestMessage = new ShowDesignationListRequestMessage()
        {
          EmployeeType = EmployeeTypeEnum.Teacher
        };
        DesignationListViewModel showDesignationListViewListModel = new DesignationListViewModel();
        var showDesignationListResponse =
          _showDesignationListRequestHandler.Handle(showDesignationListRequestMessage, cancellationToken);
        var pickedDesignationListViewModel = _showDesignationListResponsePresenter.Handle(
          showDesignationListResponse.Result, showDesignationListViewListModel, GetUserAssociationResponseMessage());

        var showEmployeeGradeListRequestMessage = new ShowEmployeeGradeListRequestMessage();
        GradeListViewModel showEmployeeGradeListViewListModel = new GradeListViewModel();
        var showEmployeeGradeListResponse =
          _showEmployeeGradeListRequestHandler.Handle(showEmployeeGradeListRequestMessage, cancellationToken);
        var pickedEmployeeGradeListViewModel = _showEmployeeGradeListResponsePresenter.Handle(
          showEmployeeGradeListResponse.Result, showEmployeeGradeListViewListModel,
          GetUserAssociationResponseMessage());

        viewModel.Branches = pickedBranchMediumListViewModel;
        viewModel.Employees = pickedEmployeeListViewModel.EmployeeList;
        viewModel.JobTypes = pickedJobTypeListViewModel.JobTypes;
        viewModel.Designations = pickedDesignationListViewModel.Designations;
        viewModel.Grades = pickedEmployeeGradeListViewModel.Grades;
        return View(viewModel);
      }
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Employee, (int) Feature.EmployeeEnum.NewEmployee)]
    [HttpGet]
    public ActionResult AddStaff(AssociationType associationType, long? objectId)
    {
      var cancellationToken = new CancellationToken();

      GetUserAssociationResponseMessage userAssociationMessage = GetUserAssociationResponseMessage();
      var model = new AddEmployeeViewModel()
      {
        LoginName = userAssociationMessage.LoginName,
        AssociatedWith = userAssociationMessage.AssociatedWith,
        LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId,
        LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId,
        LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId,
        RoleFeatures = userAssociationMessage.RoleFeatures,
        InstituteBanner = userAssociationMessage.InstituteBanner
      };

      var showEmployeeListRequestMessage = new ShowEmployeeListRequestMessage();
      var showEmployeeListViewListModel = new EmployeeListViewModel();
      showEmployeeListRequestMessage.ObjectId = (long) objectId;
      showEmployeeListRequestMessage.AssociationType = associationType;
      var showEmployeeListResponse =
        _showEmployeeListRequestHandler.Handle(showEmployeeListRequestMessage, cancellationToken);
      var pickedEmployeeListViewModel = _showEmployeeListResponsePresenter.Handle(showEmployeeListResponse.Result,
        showEmployeeListViewListModel, GetUserAssociationResponseMessage());

      var showJobTypeListRequestMessage = new ShowJobTypeListRequestMessage();
      var showJobTypeListViewListModel = new JobTypeListViewModel();
      var showJobTypeListResponse =
        _showJobTypeListRequestHandler.Handle(showJobTypeListRequestMessage, cancellationToken);
      var pickedJobTypeListViewModel = _showJobTypeListResponsePresenter.Handle(showJobTypeListResponse.Result,
        showJobTypeListViewListModel, GetUserAssociationResponseMessage());

      var showDesignationListRequestMessage = new ShowDesignationListRequestMessage()
      {
        EmployeeType = EmployeeTypeEnum.Staff
      };
      var showDesignationListViewListModel = new DesignationListViewModel();
      var showDesignationListResponse =
        _showDesignationListRequestHandler.Handle(showDesignationListRequestMessage, cancellationToken);
      var pickedDesignationListViewModel = _showDesignationListResponsePresenter.Handle(
        showDesignationListResponse.Result, showDesignationListViewListModel, GetUserAssociationResponseMessage());

      var showEmployeeGradeListRequestMessage = new ShowEmployeeGradeListRequestMessage();
      var showEmployeeGradeListViewListModel = new GradeListViewModel();
      var showEmployeeGradeListResponse =
        _showEmployeeGradeListRequestHandler.Handle(showEmployeeGradeListRequestMessage, cancellationToken);
      var pickedEmployeeGradeListViewModel = _showEmployeeGradeListResponsePresenter.Handle(
        showEmployeeGradeListResponse.Result, showEmployeeGradeListViewListModel, GetUserAssociationResponseMessage());

      model.ObjectId = objectId;
      model.AssociationType = associationType;
      model.Employees = pickedEmployeeListViewModel.EmployeeList;
      model.JobTypes = pickedJobTypeListViewModel.JobTypes;
      model.Designations = pickedDesignationListViewModel.Designations;
      model.Grades = pickedEmployeeGradeListViewModel.Grades;
      return View(model);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Employee, (int) Feature.EmployeeEnum.NewEmployee)]
    [HttpPost]
    public ActionResult AddStaff(AddEmployeeViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new AddEmployeeRequestMessage();
      requestMessage.BankAccountNo = model.BankAccountNo;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;
      requestMessage.DesignationId = model.DesignationId;
      requestMessage.DOB = model.DOB;
      requestMessage.EmailAddress = model.EmailAddress;
      requestMessage.EmployeeType = EmployeeTypeEnum.Staff;
      requestMessage.FullName = model.FullName;
      requestMessage.GradeId = model.GradeId;
      requestMessage.JobTypeId = model.JobTypeId;
      requestMessage.JoiningDate = model.JoiningDate;
      requestMessage.MobileNo = model.MobileNo;
      requestMessage.NationalId = model.NationalId;
      requestMessage.AssociationType = model.AssociationType;
      requestMessage.ObjectId = model.ObjectId;
      requestMessage.ReportToId = model.ReportToId;
      var response = _addEmployeeRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel =
        _addEmployeeResponsePresenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());

      if (response.Result.EmployeeId > 0)
      {
        return RedirectToAction("ViewBranchMedium", "BranchMedium",
              new { branchMediumId = model.ObjectId, activeTab = TabEnum.Employee });
      }
      else
      {
        var showBranchMediumListRequestMessage = new ShowBranchMediumListRequestMessage();
        List<BranchMediumViewModel> showBranchViewListModel = new List<BranchMediumViewModel>();
        var showBranchMediumListResponse =
          _showBranchMediumListRequestHandler.Handle(showBranchMediumListRequestMessage, cancellationToken);
        var pickedBranchMediumListViewModel =
          _showBranchMediumListResponsePresenter.Handle(showBranchMediumListResponse.Result, showBranchViewListModel);

        var showEmployeeListRequestMessage = new ShowEmployeeListRequestMessage();
        showEmployeeListRequestMessage.AssociationType = model.AssociationType;
        EmployeeListViewModel showemployeeListViewListModel = new EmployeeListViewModel();
        var showEmployeeListResponse =
          _showEmployeeListRequestHandler.Handle(showEmployeeListRequestMessage, cancellationToken);
        var pickedEmployeeListViewModel = _showEmployeeListResponsePresenter.Handle(showEmployeeListResponse.Result,
          showemployeeListViewListModel, GetUserAssociationResponseMessage());

        var showJobTypeListRequestMessage = new ShowJobTypeListRequestMessage();
        JobTypeListViewModel showJobTypeListViewListModel = new JobTypeListViewModel();
        var showJobTypeListResponse =
          _showJobTypeListRequestHandler.Handle(showJobTypeListRequestMessage, cancellationToken);
        var pickedJobTypeListViewModel = _showJobTypeListResponsePresenter.Handle(showJobTypeListResponse.Result,
          showJobTypeListViewListModel, GetUserAssociationResponseMessage());

        var showDesignationListRequestMessage = new ShowDesignationListRequestMessage()
        {
          EmployeeType = EmployeeTypeEnum.Staff
        };
        DesignationListViewModel showDesignationListViewListModel = new DesignationListViewModel();
        var showDesignationListResponse =
          _showDesignationListRequestHandler.Handle(showDesignationListRequestMessage, cancellationToken);
        var pickedDesignationListViewModel = _showDesignationListResponsePresenter.Handle(
          showDesignationListResponse.Result, showDesignationListViewListModel, GetUserAssociationResponseMessage());

        var showEmployeeGradeListRequestMessage = new ShowEmployeeGradeListRequestMessage();
        GradeListViewModel showEmployeeGradeListViewListModel = new GradeListViewModel();
        var showEmployeeGradeListResponse =
          _showEmployeeGradeListRequestHandler.Handle(showEmployeeGradeListRequestMessage, cancellationToken);
        var pickedEmployeeGradeListViewModel = _showEmployeeGradeListResponsePresenter.Handle(
          showEmployeeGradeListResponse.Result, showEmployeeGradeListViewListModel,
          GetUserAssociationResponseMessage());

        viewModel.Branches = pickedBranchMediumListViewModel;
        viewModel.Employees = pickedEmployeeListViewModel.EmployeeList.ToList();
        viewModel.JobTypes = pickedJobTypeListViewModel.JobTypes;
        viewModel.Designations = pickedDesignationListViewModel.Designations;
        viewModel.Grades = pickedEmployeeGradeListViewModel.Grades;
        return View(viewModel);
      }
    }

    [HttpPost]
    public JsonResult GetDesignationsByEmployeeType(EmployeeTypeEnum employeeType)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowDesignationListRequestMessage();
      requestMessage.EmployeeType = employeeType;
      var model = new List<DesignationViewModel>();

      var designationList = _showDesignationListRequestHandler.Handle(requestMessage, cancellationToken).Result
        .DesignationList;
      return Json(designationList);
    }

    public ActionResult LoadEmployeeEducationalQualificationPartialView(long employeeId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new GetEmployeeEducationalQualificationsRequestMessage
      {
        EmployeeId = employeeId
      };
      var response = _getEmployeeEducationalQualificationsRequestHandler.Handle(requestMessage, cancellationToken);
      var employeeEducationalQualificationsViewModel = _showEmployeeEducationalQualificationsResponsePresenter
        .Handle(response.Result, new EmployeeEducationalQualificationsViewModel());
      employeeEducationalQualificationsViewModel.EmployeeId = employeeId;
      return PartialView("EmployeeEducationalQualification", employeeEducationalQualificationsViewModel);
    }

    public ActionResult LoadEmployeeAddressPartialView(long employeeId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new GetEmployeeAddressRequestMessage
      {
        EmployeeId = employeeId
      };
      var response = _getEmployeeAddressRequestHandler.Handle(requestMessage, cancellationToken);
      var employeeAddressModel = _showEmployeeAddressResponsePresenter.Handle(response.Result, new AddressViewModel());

      return PartialView("EmployeeAddress", employeeAddressModel);
    }

    public JsonResult GetEmployeeAttendance(long employeeId)
    {
      if (employeeId > 0)
      {
        var cancellationToken = new CancellationToken();
        var requestMessage = new ShowCurrentMonthAttendanceListRequestMessage
        {
          EmployeeId = employeeId
        };
        var response = _showCurrentMonthAttendanceListRequestHandler.Handle(requestMessage, cancellationToken);
        return Json(response.Result.AttendanceList);
      }
      else
      {
        return Json(new {employeeIdErrorMessage = "Invalid Employee Id : " + employeeId});
      }
    }

    public ActionResult LoadEmployeePersonalInfoPartialView(long employeeId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new GetEmployeePersonalInfoRequestMessage
      {
        EmployeeId = employeeId
      };
      var response = _getEmployeePersonalInfoRequestHandler.Handle(requestMessage, cancellationToken);
      var employeePersonalInfoModel = _showEmployeePersonalInfoResponsePresenter.Handle(response.Result,
        new EmployeePersonalInfoViewModel());

      return PartialView("EmployeePersonalInfo", employeePersonalInfoModel);
    }

    [HttpPost]
    public JsonResult GetEmployeeAttendanceByDateRange(long employeeId, DateTime? startDate, DateTime? endDate)
    {
      if (employeeId > 0)
      {
        if (startDate != null && endDate != null)
        {
          if (startDate > endDate)
            return Json(new {invalidDateRangeErrorMessage = "End Date must be greater than Start Date"});
          var cancellationToken = new CancellationToken();
          var requestMessage = new GetAttendanceListByDateRangeRequestMessage
          {
            EmployeeId = employeeId,
            AttendanceEndDate = (DateTime) endDate,
            AttendanceStartDate = (DateTime) startDate
          };
          var response = _getAttendanceListByDateRange.Handle(requestMessage, cancellationToken);
          if (response.Result.AttendanceList != null && response.Result.AttendanceList.Any())
          {
            return Json(response.Result.AttendanceList);
          }
        }
        else
        {
          return Json(new {enterDateRangeErrorMessage = "Enter Date Range"});
        }
      }
      else
      {
        return Json(new {employeeIdErrorMessage = "Invalid Employee Id : " + employeeId});
      }

      return null;
    }

    public JsonResult LoadEmployeeLeaves(long employeeId)
    {
      if (employeeId > 0)
      {
        var employeeLeaves = GetEmployeeLeaves(employeeId);
        return Json(employeeLeaves);
      }

      return Json(new {employeeIdErrorMessage = "Invalid Employee Id : " + employeeId});
    }

    public JsonResult SearchEmployeeLeaves(long employeeId, DateTime? startDate, DateTime? endDate)
    {
      if (employeeId > 0)
      {
        if (startDate != null && endDate != null)
        {
          if (startDate > endDate)
            return Json(new {invalidDateRangeErrorMessage = "End Date must be greater than Start Date"});
          return Json(GetEmployeeLeaves(employeeId, startDate, endDate));
        }

        return Json(new {enterDateRangeErrorMessage = "Enter Date Range"});
      }

      return Json(new {employeeIdErrorMessage = "Invalid Employee Id : " + employeeId});
    }

    private IEnumerable<EmployeeLeaveMessageModel> GetEmployeeLeaves(long employeeId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new GetEmployeeLeavesRequestMessage();
      requestMessage.EmployeeId = employeeId;
      var response = _getEmployeeLeavesRequestHandler.Handle(requestMessage, cancellationToken);
      return response.Result.EmployeeLeaves;
    }

    private IEnumerable<EmployeeLeaveMessageModel> GetEmployeeLeaves(long employeeId, DateTime? startDate,
      DateTime? endDate)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new GetEmployeeLeavesRequestMessage();
      requestMessage.EmployeeId = employeeId;
      requestMessage.StartDate = startDate;
      requestMessage.EndDate = endDate;
      var response = _getEmployeeLeavesRequestHandler.Handle(requestMessage, cancellationToken);
      return response.Result.EmployeeLeaves;
    }

    public JsonResult GetEmployeePendingLeaves(long employeeId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new GetPendingLeavesRequestMessage();
      requestMessage.EmployeeId = employeeId;
      var response = _getPendingLeavesRequestHandler.Handle(requestMessage, cancellationToken);
      return Json(response.Result.PendingLeaves);
    }
  }
}