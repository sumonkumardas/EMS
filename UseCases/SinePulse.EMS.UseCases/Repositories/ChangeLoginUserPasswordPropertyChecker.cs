using Microsoft.AspNetCore.Identity;
using SinePulse.EMS.Domain.UserManagement;

namespace SinePulse.EMS.UseCases.Repositories
{
  public class ChangeLoginUserPasswordPropertyChecker : IChangeLoginUserPasswordPropertyChecker
  {
    private readonly UserManager<LoginUser> _userManager;

    public ChangeLoginUserPasswordPropertyChecker(UserManager<LoginUser> userManager)
    {
      _userManager = userManager;
    }

    public bool IsOldPasswordMatch(string oldPassword, string employeeCode)
    {
      var loginUser = _userManager.FindByNameAsync(employeeCode).Result;
      return _userManager.CheckPasswordAsync(loginUser, oldPassword).Result;
    }

    public bool IsUserExists(string employeeCode)
    {
      var loginUser = _userManager.FindByNameAsync(employeeCode).Result;
      return loginUser != null;
    }
  }
}