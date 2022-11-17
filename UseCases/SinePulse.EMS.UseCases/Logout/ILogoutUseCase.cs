using SinePulse.EMS.Messages.LogoutMessages;

namespace SinePulse.EMS.UseCases.Logout
{
  public interface ILogoutUseCase
  {
    LogoutResponseMessage Logout(LogoutRequestMessage requestMessage);
  }
}