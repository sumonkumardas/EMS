using Microsoft.AspNetCore.Identity;
using SinePulse.EMS.Domain.UserManagement;

namespace SinePulse.EMS.UseCases.Repositories
{
  public class ValidUsernameChecker : IValidUsernameChecker
  {
    private readonly UserManager<LoginUser> _userManager;

    public ValidUsernameChecker(UserManager<LoginUser> userManager)
    {
      _userManager = userManager;
    }

    public bool IsValidUsername(string username)
    {
      var user = _userManager.FindByNameAsync(username).Result;
      return user != null;
    }
  }
}