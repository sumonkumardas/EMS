using System;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.UseCases.Repositories
{
  public interface ITermOverlappingChecker
  {
    bool IsNonOverlappingWithSessionStartTime(DateTime startDate, long sessionId, long branchMediumId);
    bool IsNonOverlappingWithSessionEndTime(DateTime endDate, long sessionId, long branchMediumId);

    bool IsNonOverlappingWithTermStartTime(DateTime startDate, long sessionId,long branchMediumId);
    bool IsNonOverlappingWithTermEndTime(DateTime endDate, long sessionId,long branchMediumId);
  }
}