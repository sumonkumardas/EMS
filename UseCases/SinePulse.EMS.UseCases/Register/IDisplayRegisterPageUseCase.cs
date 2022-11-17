using SinePulse.EMS.Messages.RegisterMessages;

namespace SinePulse.EMS.UseCases.Register
{
  public interface IDisplayRegisterPageUseCase
  {
    DisplayRegisterPageResponseMessage DisplayRegisterPage(DisplayRegisterPageRequestMessage requestMessage);
  }
}