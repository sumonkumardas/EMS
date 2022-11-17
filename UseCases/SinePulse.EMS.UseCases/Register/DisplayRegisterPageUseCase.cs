using SinePulse.EMS.Messages.RegisterMessages;

namespace SinePulse.EMS.UseCases.Register
{
  public class DisplayRegisterPageUseCase : IDisplayRegisterPageUseCase
  {
    public DisplayRegisterPageResponseMessage DisplayRegisterPage(DisplayRegisterPageRequestMessage requestMessage)
    {
      return new DisplayRegisterPageResponseMessage();
    }
  }
}