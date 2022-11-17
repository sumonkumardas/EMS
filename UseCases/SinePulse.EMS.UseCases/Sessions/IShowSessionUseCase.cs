using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Messages.SessionMessages;

namespace SinePulse.EMS.UseCases.Sessions
{
  public interface IShowSessionUseCase
  {
    SessionMessageModel ShowSession(ViewSessionRequestMessage requestMessage);
  }
}