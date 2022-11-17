using System.Linq;
using System.Text.RegularExpressions;
using FluentValidation;
using SinePulse.EMS.Messages.LoginUserMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.LoginUsers
{
  public class ChangeLoginUserPasswordRequestMessageValidator : AbstractValidator<ChangeLoginUserPasswordRequestMessage>
  {
    private readonly IChangeLoginUserPasswordPropertyChecker _changeLoginUserPasswordPropertyChecker;

    public ChangeLoginUserPasswordRequestMessageValidator(
      IChangeLoginUserPasswordPropertyChecker changeLoginUserPasswordPropertyChecker)
    {
      _changeLoginUserPasswordPropertyChecker = changeLoginUserPasswordPropertyChecker;
      RuleFor(x => x).Must((m, x) => IsUserExists(m.EmployeeCode))
        .WithMessage("User Does not exists! At first Create Login User.");
      RuleFor(x => x.OldPassword).Must((m, x) => IsOldPasswordMatch(m.OldPassword, m.EmployeeCode))
        .WithMessage("Old Password did not match!");
      RuleFor(x => x.NewPassword).MinimumLength(8).WithMessage("Password length must be greater than or equal to 8.");
      RuleFor(x => x.NewPassword)
        .Must((m, x) => IsPasswordContainsNoneAlphaNumericCharacter(m.NewPassword, m.EmployeeCode))
        .WithMessage("Password must contain a special character, a upper case letter and a number.");
      RuleFor(x => x.NewPassword).Must((m, x) => IsPasswordContainsUpperCaseLetter(m.NewPassword, m.EmployeeCode))
        .WithMessage("Password must contain a special character, a upper case letter and a number.");
      RuleFor(x => x.NewPassword).Must((m, x) => IsPasswordContainsNumber(m.NewPassword, m.EmployeeCode))
        .WithMessage("Password must contain a special character, a upper case letter and a number.");

      RuleFor(x => x.RepeatPassword).Must((m, x) => IsValidRepeatPassword(m.RepeatPassword, m.NewPassword, m.EmployeeCode))
        .WithMessage("Password did not match.");
      RuleFor(x => x.OldPassword).Must((m, x) => IsValidOldPassword(m.OldPassword, m.EmployeeCode))
        .WithMessage("Password Specify Old Password.");
      RuleFor(x => x)
        .Must((m, x) =>
          IsCurrentUserHasAuthorityToChangePassword(m.EmployeeRoleName, m.CurrentUserRoleName, m.EmployeeCode))
        .WithMessage("You do not have the authority to change password of this Employee!");
    }

    private bool IsValidRepeatPassword(string repeatPassword, string newPassword, string employeeCode)
    {
      if (_changeLoginUserPasswordPropertyChecker.IsUserExists(employeeCode))
      {
        if (newPassword != null && repeatPassword != null && repeatPassword.Equals(newPassword))
          return true;
        return false;
      }

      return true;
    }

    private bool IsValidOldPassword(string oldPassword, string employeeCode)
    {
      if (_changeLoginUserPasswordPropertyChecker.IsUserExists(employeeCode))
      {
        if (string.IsNullOrEmpty(oldPassword))
          return false;
        return true;
      }

      return true;
    }

    private bool IsUserExists(string employeeCode)
    {
      return _changeLoginUserPasswordPropertyChecker.IsUserExists(employeeCode);
    }

    private bool IsOldPasswordMatch(string oldPassword, string employeeCode)
    {
      if (string.IsNullOrEmpty(oldPassword))
        return true;
      if (_changeLoginUserPasswordPropertyChecker.IsUserExists(employeeCode))
        return _changeLoginUserPasswordPropertyChecker.IsOldPasswordMatch(oldPassword, employeeCode);
      return true;
    }

    private bool IsPasswordContainsNumber(string password, string employeeCode)
    {
      if (_changeLoginUserPasswordPropertyChecker.IsUserExists(employeeCode))
        return password != null && password.Any(char.IsDigit);
      return true;
    }

    private bool IsPasswordContainsUpperCaseLetter(string password, string employeeCode)
    {
      if (_changeLoginUserPasswordPropertyChecker.IsUserExists(employeeCode))
        return password != null && password.Any(char.IsUpper);
      return true;
    }

    private bool IsPasswordContainsNoneAlphaNumericCharacter(string password, string employeeCode)
    {
      if (_changeLoginUserPasswordPropertyChecker.IsUserExists(employeeCode))
      {
        var regexForAlphaNumeric = new Regex(@"^[a-zA-Z0-9\s,]*$");
        return password != null && !regexForAlphaNumeric.IsMatch(password);
      }

      return true;
    }

    private bool IsCurrentUserHasAuthorityToChangePassword(string roleName, string currentUserRoleName,
      string employeeCode)
    {
      if (_changeLoginUserPasswordPropertyChecker.IsUserExists(employeeCode))
      {
        if (GetUserRoleEnumValue(currentUserRoleName) > GetUserRoleEnumValue(roleName) &&
            !string.IsNullOrEmpty(roleName))
        {
          return false;
        }
      }

      return true;
    }

    private int GetUserRoleEnumValue(string userName)
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