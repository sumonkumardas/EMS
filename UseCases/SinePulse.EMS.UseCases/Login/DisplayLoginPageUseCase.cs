using SinePulse.EMS.Messages.LoginMessages;

namespace SinePulse.EMS.UseCases.Login
{
  public class DisplayLoginPageUseCase : IDisplayLoginPageUseCase
  {
    public DisplayLoginPageResponseMessage DisplayLoginPage(DisplayLoginPageRequestMessage requestMessage)
    {
      return new DisplayLoginPageResponseMessage();
    }
  }
}