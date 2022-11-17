using System;

namespace SinePulse.EMS.UseCases.Repositories
{
  public interface IUniqueSessionPropertyChecker
  {
    bool IsUniqueSessionName(long branchMediumId, string sessionName);
    bool IsSessionExists(DateTime sessionStartDate, DateTime sessionEndDate, long sessionId);
    bool IsUniqueSessionName(string sessionName, long sessionId);
    bool IsSessionExists(long sessionId);
  }
}