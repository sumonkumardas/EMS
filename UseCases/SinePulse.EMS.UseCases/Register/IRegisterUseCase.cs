using SinePulse.EMS.Messages.RegisterMessages;

namespace SinePulse.EMS.UseCases.Register
{
  public interface IRegisterUseCase
  {
    RegisterResponseMessage Register(RegisterRequestMessage requestMessage);
  }
}