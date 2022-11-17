using SinePulse.EMS.Messages.LoginMessages;

namespace SinePulse.EMS.UseCases.Login
{
  public interface IDisplayLoginPageUseCase
  {
    DisplayLoginPageResponseMessage DisplayLoginPage(DisplayLoginPageRequestMessage requestMessage);
  }
}