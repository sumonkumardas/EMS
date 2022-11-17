using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SinePulse.EMS.Domain.UserManagement;
using SinePulse.EMS.Messages.BranchMediumMessages;
using SinePulse.EMS.Messages.BranchMessages;
using SinePulse.EMS.Messages.EmployeeMessages;
using SinePulse.EMS.Messages.LoginUserMessages;
using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Messages.Model.Employees;
using SinePulse.EMS.Messages.RoleMessages;
using SinePulse.EMS.ProjectPrimitives;
using SinePulse.EMS.Site.Models;
using SinePulse.EMS.Site.Presenters;
using SinePulse.EMS.UseCases.Authorization;
using SinePulse.EMS.UseCases.Branches;
using SinePulse.EMS.UseCases.BranchMediums;
using SinePulse.EMS.UseCases.Employee;
using SinePulse.EMS.UseCases.LoginUsers;
using SinePulse.EMS.UseCases.Roles;

namespace SinePulse.EMS.Site.Controllers
{
  public class LoginUserController : BaseController
  {
    private readonly UserManager<LoginUser> _userManager;
    private readonly AddLoginUserRequestHandler _addLoginUserRequestHandler;
    private readonly AddLoginUserResponsePresenter _addLoginUserResponsePresenter;
    private readonly ChangeLoginUserPasswordRequestHandler _changeLoginUserPasswordRequestHandler;
    private readonly ChangeLoginUserPasswordResponsePresenter _changeLoginUserPasswordLoginUserResponsePresenter;
    private readonly ShowRoleListRequestHandler _showRoleListRequestHandler;
    private readonly ShowRoleRequestHandler _showRoleRequestHandler;
    private readonly ShowEmployeeRequestHandler _showEmployeeRequestHandler;
    private readonly ShowBranchListRequestHandler _showBranchListRequestHandler;
    private readonly ShowBranchMediumListRequestHandler _showBranchMediumListRequestHandler;

    public LoginUserController(GetUserAssociationRequestHandler getUserAssociationRequestHandler, 
      UserManager<LoginUser> userManager, 
      AddLoginUserRequestHandler addLoginUserRequestHandler, 
      AddLoginUserResponsePresenter addLoginUserResponsePresenter, 
      ChangeLoginUserPasswordRequestHandler changeLoginUserPasswordRequestHandler, 
      ChangeLoginUserPasswordResponsePresenter changeLoginUserPasswordLoginUserResponsePresenter, 
      ShowRoleListRequestHandler showRoleListRequestHandler, 
      ShowRoleRequestHandler showRoleRequestHandler, 
      ShowEmployeeRequestHandler showEmployeeRequestHandler, 
      ShowBranchListRequestHandler showBranchListRequestHandler, 
      ShowBranchMediumListRequestHandler showBranchMediumListRequestHandler) : base(getUserAssociationRequestHandler)
    {
      _userManager = userManager;
      _addLoginUserRequestHandler = addLoginUserRequestHandler;
      _addLoginUserResponsePresenter = addLoginUserResponsePresenter;
      _changeLoginUserPasswordRequestHandler = changeLoginUserPasswordRequestHandler;
      _changeLoginUserPasswordLoginUserResponsePresenter = changeLoginUserPasswordLoginUserResponsePresenter;
      _showRoleListRequestHandler = showRoleListRequestHandler;
      _showRoleRequestHandler = showRoleRequestHandler;
      _showEmployeeRequestHandler = showEmployeeRequestHandler;
      _showBranchListRequestHandler = showBranchListRequestHandler;
      _showBranchMediumListRequestHandler = showBranchMediumListRequestHandler;
    }
    
    [HttpGet]
    public ActionResult AddLoginUser(long employeeId)
    {
      var userAssociationMessage = GetUserAssociationResponseMessage();
      return View(new AddLoginUserViewModel
      {
        EmployeeId = employeeId,
        LoginName = userAssociationMessage.LoginName,
        AssociatedWith = userAssociationMessage.AssociatedWith,
        LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId,
        LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId,
        LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId,
        RoleFeatures = userAssociationMessage.RoleFeatures,
        InstituteBanner = userAssociationMessage.InstituteBanner,
        UserRoles = GetLoginUserRoles()
      });
    }

    [HttpPost]
    public ActionResult AddLoginUser(AddLoginUserViewModel model)
    {
      var userAssociationMessage = GetUserAssociationResponseMessage();
      var loginUser = _userManager.FindByNameAsync(HttpContext.User.Identity.Name).Result;
      var cancellationToken = new CancellationToken();
      var requestMessage = new AddLoginUserRequestMessage();
      requestMessage.EmployeeId = model.EmployeeId;
      requestMessage.CurrentUserRoleName =  _userManager.GetRolesAsync(loginUser).Result.FirstOrDefault();
      requestMessage.Password = model.Password;
      requestMessage.RepeatPassword = model.RepeatPassword;
      requestMessage.RoleId = model.RoleId;
      requestMessage.InstituteId = model.InstituteId;
      requestMessage.BranchId = model.BranchId;
      requestMessage.BranchMediumId = model.BranchMediumId;
      
      var response = _addLoginUserRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _addLoginUserResponsePresenter.Handle(response.Result, model, ModelState, userAssociationMessage);

      if (!response.Result.ValidationResult.IsValid)
      {
        viewModel.EmployeeId = model.EmployeeId;
        viewModel.UserRoles = GetLoginUserRoles();
        viewModel.InstituteId = model.InstituteId;
        viewModel.BranchId = model.BranchId;
        viewModel.BranchMediumId = model.BranchMediumId;
        return View(viewModel);
      }

      return RedirectToAction("ViewEmployee", "Employee", new {employeeId = model.EmployeeId});
    }

    public JsonResult PopulateDropdownsOfLoginUser(string roleId, long employeeId)
    {
      if (string.IsNullOrEmpty(roleId) || employeeId <= 0)
        return Json(new {InvalidParameter = true});
      var roleType = GetRoleType(roleId);
      var employee = GetEmployee(employeeId);
      var institutes = new List<InstituteMessageModel> {employee.Institute};
      var branches = new List<BranchMessageModel>{employee.Branch};
      if (employee.Branch == null)
        branches = GetBranches(employee.Institute.Id);
      var branchMediums = new List<BranchMediumMessageModel>{employee.BranchMedium};
      if (employee.BranchMedium == null && employee.Branch != null)
        branchMediums = GetBranchMediums(employee.Branch.Id);
      switch (roleType)
      {
        case RoleTypeEnum.SuperAdmin:
          return Json(new {RoleType = "SuperAdmin"});
        case RoleTypeEnum.InstituteAdmin:
          return Json(new
          {
            RoleType = "InstituteAdmin",
            Institutes = institutes
          });
        case RoleTypeEnum.BranchAdmin:
          return Json(new
          {
            RoleType = "BranchAdmin",
            Institutes = institutes,
            Branches = new List<BranchMessageModel>(branches)
          });
        case RoleTypeEnum.BranchMediumAdmin:
          return Json(new {RoleType = "BranchMediumAdmin",
            Institutes = institutes,
            Branches = branches,
            BranchMediums = branchMediums
          });
        case RoleTypeEnum.BranchMediumUser:
          return Json(new
          {
            RoleType = "BranchMediumUser",
            Institutes = institutes,
            Branches = branches,
            BranchMediums = branchMediums
          });
        default:
          return Json(new {NoData = true});
      }
    }

    public JsonResult GetBranchMediumList(long branchId, long employeeId)
    {
      var employee = GetEmployee(employeeId);
      if (employee.BranchMedium != null)
        return Json(new List<BranchMediumMessageModel> {employee.BranchMedium});
      if (branchId > 0)
      {
        return Json(GetBranchMediums(branchId));
      }

      return Json(null);
    }
    private List<BranchMediumMessageModel> GetBranchMediums(long branchId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowBranchMediumListRequestMessage();
      requestMessage.BranchId = branchId;
      var response = _showBranchMediumListRequestHandler.Handle(requestMessage, cancellationToken);
      return response.Result.BranchMediumList;
    }

    private List<BranchMessageModel> GetBranches(long instituteId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowBranchListRequestMessage();
      requestMessage.InstituteId = instituteId;
      var response = _showBranchListRequestHandler.Handle(requestMessage, cancellationToken);
      return response.Result.Branches as List<BranchMessageModel>;
    }

    private EmployeeMessageModel GetEmployee(long employeeId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowEmployeeRequestMessage();
      requestMessage.EmployeeId = employeeId;
      var response = _showEmployeeRequestHandler.Handle(requestMessage, cancellationToken);
      return response.Result.Employee;
    }

    private RoleTypeEnum GetRoleType(string roleId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowRoleRequestMessage();
      requestMessage.RoleId = roleId;
      var response = _showRoleRequestHandler.Handle(requestMessage, cancellationToken);
      return response.Result.Data.RoleData.RoleType;
    }

    private ICollection<ShowRoleListResponseMessage.Role> GetLoginUserRoles()
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowRoleListRequestMessage();
      var response = _showRoleListRequestHandler.Handle(requestMessage, cancellationToken);
      return response.Result.Data.Roles;
    }

    [HttpGet]
    public ActionResult ChangeLoginUserPassword(string employeeCode, long employeeId)
    {
      var userAssociationMessage = GetUserAssociationResponseMessage();
      return View(new ChangeLoginUserPasswordViewModel
      {
        EmployeeCode = employeeCode,
        EmployeeId = employeeId,
        LoginName = userAssociationMessage.LoginName,
        AssociatedWith = userAssociationMessage.AssociatedWith,
        LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId,
        LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId,
        LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId,
        RoleFeatures = userAssociationMessage.RoleFeatures,
        InstituteBanner = userAssociationMessage.InstituteBanner
      });
    }
    
    [HttpPost]
    public ActionResult ChangeLoginUserPassword(ChangeLoginUserPasswordViewModel model)
    {
      var userAssociationMessage = GetUserAssociationResponseMessage();
      var cancellationToken = new CancellationToken();
      var currentLoginUser = _userManager.FindByNameAsync(HttpContext.User.Identity.Name).Result;
      var employeeLoginUser = _userManager.FindByNameAsync(model.EmployeeCode).Result;
      var requestMessage = new ChangeLoginUserPasswordRequestMessage();
      requestMessage.CurrentUserRoleName = _userManager.GetRolesAsync(currentLoginUser).Result.FirstOrDefault();
      if (employeeLoginUser != null)
        requestMessage.EmployeeRoleName = _userManager.GetRolesAsync(employeeLoginUser).Result.FirstOrDefault();
      requestMessage.EmployeeCode = model.EmployeeCode;
      requestMessage.OldPassword = model.OldPassword;
      requestMessage.NewPassword = model.NewPassword;
      requestMessage.RepeatPassword = model.RepeatPassword;
      var response = _changeLoginUserPasswordRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _changeLoginUserPasswordLoginUserResponsePresenter.Handle(response.Result, model, ModelState, 
        userAssociationMessage);

      if (!response.Result.ValidationResult.IsValid)
      {
        viewModel.EmployeeId = model.EmployeeId;
        viewModel.EmployeeCode = model.EmployeeCode;
        return View(viewModel);
      }

      return RedirectToAction("ViewEmployee", "Employee", new {employeeId = model.EmployeeId});
    }
  }
}