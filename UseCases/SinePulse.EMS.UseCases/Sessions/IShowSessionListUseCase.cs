using System.Collections.Generic;
using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Messages.SessionMessages;

namespace SinePulse.EMS.UseCases.Sessions
{
  public interface IShowSessionListUseCase
  {
    IEnumerable<SessionMessageModel> ShowSessionList(ShowSessionListRequestMessage requestMessage);
  }
}