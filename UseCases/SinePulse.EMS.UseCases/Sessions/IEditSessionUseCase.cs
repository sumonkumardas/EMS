using SinePulse.EMS.Messages.SessionMessages;

namespace SinePulse.EMS.UseCases.Sessions
{
  public interface IEditSessionUseCase
  {
    void UpdateSession(EditSessionRequestMessage requestMessage);
  }
}