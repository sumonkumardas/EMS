using System;

namespace SinePulse.EMS.UseCases.Repositories
{
  public interface IUniqueShiftPropertyChecker
  {
    bool IsUniqueShiftName(string shiftName, long branchId);
    bool IsShiftStartAndEndTimeUnique(TimeSpan startTime, TimeSpan endTime, long branchId);
    bool IsUniqueShiftName(string shiftName, long branchId, long shiftId);
    bool IsUniqueShiftTime(TimeSpan startTime, TimeSpan endTime, long branchId, long shiftId);
  }
}