using Microsoft.AspNetCore.Identity;
using SinePulse.EMS.Domain.UserManagement;
using SinePulse.EMS.Messages.LogoutMessages;

namespace SinePulse.EMS.UseCases.Logout
{
  public class LogoutUseCase : ILogoutUseCase
  {
    private readonly SignInManager<LoginUser> _signInManager;

    public LogoutUseCase(SignInManager<LoginUser> signInManager)
    {
      _signInManager = signInManager;
    }

    public LogoutResponseMessage Logout(LogoutRequestMessage requestMessage)
    {
      _signInManager.SignOutAsync().GetAwaiter().GetResult();
      return new LogoutResponseMessage(true);
    }
  }
}