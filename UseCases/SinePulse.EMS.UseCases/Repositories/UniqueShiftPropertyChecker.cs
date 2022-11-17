using System;
using System.Linq;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Repositories
{
  public class UniqueShiftPropertyChecker : IUniqueShiftPropertyChecker
  {
    private readonly EmsDbContext _dbContext;

    public UniqueShiftPropertyChecker(EmsDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public bool IsUniqueShiftName(string shiftName, long branchId)
    {
      return !_dbContext.Shifts
        .Where(s => s.Branch.Id == branchId)
        .Any(s => s.ShiftName == shiftName);
    }

    public bool IsShiftStartAndEndTimeUnique(TimeSpan startTime, TimeSpan endTime, long branchId)
    {
      return !_dbContext.Shifts
        .Where(s => s.Branch.Id == branchId)
        .Any(s => s.StartTime == startTime && s.EndTime == endTime);
    }

    public bool IsUniqueShiftName(string shiftName, long branchId, long shiftId)
    {
      return !_dbContext.Shifts
        .Where(s => s.Branch.Id == branchId)
        .Any(s => s.ShiftName == shiftName && s.Id != shiftId);
    }

    public bool IsUniqueShiftTime(TimeSpan startTime, TimeSpan endTime, long branchId, long shiftId)
    {
      return !_dbContext.Shifts
        .Where(s => s.Branch.Id == branchId)
        .Any(s => s.StartTime == startTime && s.EndTime == endTime && s.Id != shiftId);
    }
  }
}