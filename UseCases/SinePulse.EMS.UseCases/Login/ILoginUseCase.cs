using SinePulse.EMS.Messages.LoginMessages;

namespace SinePulse.EMS.UseCases.Login
{
  public interface ILoginUseCase
  {
    LoginResponseMessage Login(LoginRequestMessage requestMessage);
  }
}