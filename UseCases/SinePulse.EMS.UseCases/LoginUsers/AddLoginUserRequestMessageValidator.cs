using System.Linq;
using System.Text.RegularExpressions;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using SinePulse.EMS.Domain.UserManagement;
using SinePulse.EMS.Messages.LoginUserMessages;
using SinePulse.EMS.ProjectPrimitives;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.LoginUsers
{
  public class AddLoginUserRequestMessageValidator : AbstractValidator<AddLoginUserRequestMessage>
  {
    private readonly IAddLoginUserPropertyChecker _addLoginUserPropertyChecker;
    private readonly RoleManager<Role> _roleManager;

    public AddLoginUserRequestMessageValidator(IAddLoginUserPropertyChecker addLoginUserPropertyChecker, RoleManager<Role> roleManager)
    {
      _addLoginUserPropertyChecker = addLoginUserPropertyChecker;
      _roleManager = roleManager;
      RuleFor(x => x.Password).NotEmpty().WithMessage("Please Specify Password.");
      RuleFor(x => x.Password).NotNull().WithMessage("Please Specify Password.");
      RuleFor(x => x.Password).MinimumLength(8).WithMessage("Password length must be greater than or equal to 8.");
      RuleFor(x => x.Password).Must((m, x) => IsPasswordContainsNoneAlphaNumericCharacter(m.Password))
        .WithMessage("Password must contain a special character, a upper case letter and a number.");
      RuleFor(x => x.Password).Must((m, x) => IsPasswordContainsUpperCaseLetter(m.Password))
        .WithMessage("Password must contain a special character, a upper case letter and a number.");
      RuleFor(x => x.Password).Must((m, x) => IsPasswordContainsNumber(m.Password))
        .WithMessage("Password must contain a special character, a upper case letter and a number.");
      RuleFor(x => x).Must((m, x) => IsValidCurrentUserRole(m.CurrentUserRoleName))
        .WithMessage("Current User Role not found..!");
      RuleFor(x => x.RoleId).Must((m, x) => IsValidRole(m.RoleId, m.CurrentUserRoleName))
        .WithMessage("Selected user role is superior than yours. Select a role, same or less superior than yours");
      RuleFor(x => x).Must((m, x) => IsLoginUserExists(m.EmployeeId)).WithMessage("Login User Already Exists!");
      RuleFor(x => x.RepeatPassword).Must((m, x) => IsRepeatedPasswordMatched(m.RepeatPassword, m.Password))
        .WithMessage("Password did not match.");
      RuleFor(x => x.RepeatPassword).Must((m, x) => IsRepeatedEntered(m.RepeatPassword, m.Password))
        .WithMessage("Please Repeat entered Password.");
      RuleFor(x => x.InstituteId).Must((m, x) => IsValidInstituteId(m.InstituteId, m.RoleId))
        .WithMessage("Select Institute.");
      RuleFor(x => x.BranchId).Must((m, x) => IsValidBranchId(m.BranchId, m.RoleId))
        .WithMessage("Select Branch.");
      RuleFor(x => x.BranchMediumId).Must((m, x) => IsValidBranchMediumId(m.BranchMediumId, m.RoleId))
        .WithMessage("Select Medium.");
    }

    private bool IsValidBranchMediumId(string branchMediumId, string roleId)
    {
      var identityRole = _roleManager.FindByIdAsync(roleId).GetAwaiter().GetResult();
      if ((identityRole.RoleType == RoleTypeEnum.BranchMediumAdmin
           || identityRole.RoleType == RoleTypeEnum.BranchMediumUser) && string.IsNullOrEmpty(branchMediumId))
      {
        return false;
      }

      return true;
    }

    private bool IsValidBranchId(string branchId, string roleId)
    {
      var identityRole = _roleManager.FindByIdAsync(roleId).GetAwaiter().GetResult();
      if ((identityRole.RoleType == RoleTypeEnum.BranchAdmin
           || identityRole.RoleType == RoleTypeEnum.BranchMediumAdmin
           || identityRole.RoleType == RoleTypeEnum.BranchMediumUser) && string.IsNullOrEmpty(branchId))
      {
        return false;
      }

      return true;
    }

    private bool IsValidInstituteId(string instituteId, string roleId)
    {
      var identityRole = _roleManager.FindByIdAsync(roleId).GetAwaiter().GetResult();
      if ((identityRole.RoleType == RoleTypeEnum.InstituteAdmin
          || identityRole.RoleType == RoleTypeEnum.BranchAdmin
          || identityRole.RoleType == RoleTypeEnum.BranchMediumAdmin
          || identityRole.RoleType == RoleTypeEnum.BranchMediumUser) && string.IsNullOrEmpty(instituteId))
      {
        return false;
      }

      return true;
    }

    private bool IsRepeatedEntered(string repeatPassword, string password)
    {
      if (string.IsNullOrEmpty(password))
        return true;
      if (string.IsNullOrEmpty(repeatPassword))
        return false;
      return true;
    }

    private bool IsRepeatedPasswordMatched(string repeatPassword, string password)
    {
      if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(repeatPassword))
        return true;
      return repeatPassword.Equals(password);
    }

    private bool IsPasswordContainsNumber(string password)
    {
      return password != null && password.Any(char.IsDigit);
    }

    private bool IsLoginUserExists(long employeeId)
    {
      return _addLoginUserPropertyChecker.IsLoginUserAlreadyCreated(employeeId);
    }

    private bool IsPasswordContainsUpperCaseLetter(string password)
    {
      return password != null && password.Any(char.IsUpper);
    }

    private bool IsPasswordContainsNoneAlphaNumericCharacter(string password)
    {
      var regexForAlphaNumeric = new Regex(@"^[a-zA-Z0-9\s,]*$");
      return password != null && !regexForAlphaNumeric.IsMatch(password);
    }

    private bool IsValidCurrentUserRole(string currentUserRoleName)
    {
      return !string.IsNullOrEmpty(currentUserRoleName);
    }

    private bool IsValidRole(string roleId, string currentUserRoleName)
    {
      var role = _roleManager.FindByIdAsync(roleId).GetAwaiter().GetResult();
      if (string.IsNullOrEmpty(currentUserRoleName))
        return true;
      if (GetCurrentUserEnumValue(currentUserRoleName) > (int) role.RoleType)
      {
        return false;
      }

      return true;
    }

    private int GetCurrentUserEnumValue(string userName)
    {
      switch (userName)
      {
        case "SuperAdmin":
          return 1;
        case "InstituteAdmin":
          return 2;
        case "BranchAdmin":
          return 3;
        case "BranchMediumAdmin":
          return 4;
        default:
          return 0;
      }
    }
  }
}