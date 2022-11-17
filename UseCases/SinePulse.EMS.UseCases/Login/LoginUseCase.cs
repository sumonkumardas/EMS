using Microsoft.AspNetCore.Identity;
using SinePulse.EMS.Domain.UserManagement;
using SinePulse.EMS.Messages.LoginMessages;

namespace SinePulse.EMS.UseCases.Login
{
  public class LoginUseCase : ILoginUseCase
  {
    private readonly UserManager<LoginUser> _userManager;
    private readonly SignInManager<LoginUser> _signInManager;

    public LoginUseCase(UserManager<LoginUser> userManager, SignInManager<LoginUser> signInManager)
    {
      _userManager = userManager;
      _signInManager = signInManager;
    }

    public LoginResponseMessage Login(LoginRequestMessage requestMessage)
    {
      
      var user = _userManager.FindByNameAsync(requestMessage.UserName).Result;
     
      if (user != null)
      {
        var result = _signInManager.PasswordSignInAsync(user, requestMessage.Password, false, false).Result;
        if (result.Succeeded)
        {
          return new LoginResponseMessage(result.Succeeded);
        }
      }
      return new LoginResponseMessage(false);
    }
  }
}