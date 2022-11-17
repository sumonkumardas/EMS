using System.Collections.Generic;
using SinePulse.EMS.Domain.Academic;

namespace SinePulse.EMS.UseCases.Sessions
{
  public interface ISessionProvider
  {
    ICollection<Session> GetBranchMediumSpecificSessions(long branchMediumId);
    ICollection<Session> GetOnlyBranchMediumSpecificSessions(long branchMediumId);
    ICollection<Session> GetBranchSpecificSessions(long branchId);
    ICollection<Session> GetOnlyBranchSpecificSessions(long branchId);
    ICollection<Session> GetInstituteSpecificSessions(long instituteId);
    ICollection<Session> GetOnlyInstituteSpecificSessions(long instituteId);
    ICollection<Session> GetGlobalSessions();
  }
}