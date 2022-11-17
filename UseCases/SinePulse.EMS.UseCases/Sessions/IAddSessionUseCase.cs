using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Messages.SessionMessages;

namespace SinePulse.EMS.UseCases.Sessions
{
  public interface IAddSessionUseCase
  {
    SessionMessageModel AddSession(AddSessionRequestMessage request);
  }
}