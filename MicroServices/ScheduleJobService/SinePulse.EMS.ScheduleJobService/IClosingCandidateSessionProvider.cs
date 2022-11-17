using System.Collections.Generic;
using SinePulse.EMS.Domain.Academic;

namespace SinePulse.EMS.ScheduleJobService
{
  public interface IClosingCandidateSessionProvider
  {
    IEnumerable<Session> GetClosingCandidateSessions();
  }
}