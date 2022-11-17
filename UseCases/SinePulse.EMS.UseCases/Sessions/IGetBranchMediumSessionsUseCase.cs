using System.Collections.Generic;
using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Messages.SessionMessages;

namespace SinePulse.EMS.UseCases.Sessions
{
  public interface IGetBranchMediumSessionsUseCase
  {
    IEnumerable<SessionMessageModel> GetBranchMediumSessions(GetBranchMediumSessionsRequestMessage message);
  }
}