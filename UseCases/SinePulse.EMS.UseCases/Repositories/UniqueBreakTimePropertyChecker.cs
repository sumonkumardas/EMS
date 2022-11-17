using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.ProjectPrimitives;
using SinePulse.EMS.Utility;

namespace SinePulse.EMS.UseCases.Repositories
{
  public class UniqueBreakTimePropertyChecker : IUniqueBreakTimePropertyChecker
  {
    private readonly EmsDbContext _dbContext;
    private readonly ITimeOverlappingChecker _timeOverlappingChecker;

    public UniqueBreakTimePropertyChecker(EmsDbContext dbContext, ITimeOverlappingChecker timeOverlappingChecker)
    {
      _dbContext = dbContext;
      _timeOverlappingChecker = timeOverlappingChecker;
    }

    public bool IsUniqueBreakTime(TimeSpan startTime, TimeSpan endTime, IEnumerable<WeekDays> weekDays, long branchMediumId)
    {
      var weekDay = WeekDays.None;
      foreach (var day in weekDays)
      {
        if (weekDay == WeekDays.None)
        {
          weekDay = day;
        }
        else
        {
          weekDay |= day;
        }

        var overlap = _dbContext.BreakTimes.Any(bt =>
          bt.StartTime == startTime && bt.EndTime == endTime && bt.BranchMedium.Id == branchMediumId);
        if (overlap)
        {
          return false;
        }
      }
      return true;
    }

    public bool IsBreakTimeBetweenShiftTime(TimeSpan startTime, TimeSpan endTime, long branchMediumId)
    {
      var shift = _dbContext.BranchMediums
        .Include(nameof(Shift))
        .FirstOrDefault(b => b.Id == branchMediumId)?.Shift;
      
      return shift != null &&
             shift.StartTime <= startTime &&
             shift.EndTime >= endTime &&
             shift.StartTime < endTime &&
             shift.EndTime > startTime &&
             !(shift.StartTime == startTime && shift.EndTime == endTime);
    }

    public bool IsBreakTimeOverlaps(TimeSpan startTime, TimeSpan endTime, IEnumerable<WeekDays> weekDays,
      long branchMediumId)
    {
      var weekDay = WeekDays.None;
      foreach (var day in weekDays)
      {
        if (weekDay == WeekDays.None)
        {
          weekDay = day;
        }
        else
        {
          weekDay |= day;
        }

        var overlap = _dbContext.BreakTimes.Where(b => b.BranchMedium.Id == branchMediumId)
        .Any(b => DoesOverlap(b.StartTime, b.EndTime, startTime, endTime));
        
        if (overlap)
        {
          return false;
        }
      }
      return true;
    }
    
    private bool DoesOverlap(TimeSpan startTime1, TimeSpan endTime1, TimeSpan startTime2, TimeSpan endTime2)
    {
      return _timeOverlappingChecker.DoesOverlap(startTime1, endTime1, startTime2, endTime2);
    }
  }
}