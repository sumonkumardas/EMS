using SinePulse.EMS.Messages.LoginUserMessages;

namespace SinePulse.EMS.UseCases.LoginUsers
{
  public interface IChangeLoginUserPasswordUseCase
  {
    void ChangeLoginUserPassword(ChangeLoginUserPasswordRequestMessage message);
  }
}