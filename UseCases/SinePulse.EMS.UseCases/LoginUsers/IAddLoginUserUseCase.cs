using SinePulse.EMS.Messages.LoginUserMessages;

namespace SinePulse.EMS.UseCases.LoginUsers
{
  public interface IAddLoginUserUseCase
  {
    void AddLoginUser(AddLoginUserRequestMessage message);
  }
}