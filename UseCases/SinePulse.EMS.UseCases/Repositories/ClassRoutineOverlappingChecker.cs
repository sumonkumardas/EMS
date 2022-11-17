using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Utility;

namespace SinePulse.EMS.UseCases.Repositories
{
  public class ClassRoutineOverlappingChecker : IClassRoutineOverlappingChecker
  {
    private readonly EmsDbContext _dbContext;
    private readonly ITimeOverlappingChecker _timeOverlappingChecker;
    private const bool Overlaps = true;

    public ClassRoutineOverlappingChecker(EmsDbContext dbContext, ITimeOverlappingChecker timeOverlappingChecker)
    {
      _dbContext = dbContext;
      _timeOverlappingChecker = timeOverlappingChecker;
    }

    public bool IsTeacherFree(long teacherId, TimeSpan startTime, TimeSpan endTime, DayOfWeek weekDay)
    {
      return !_dbContext.ClassRoutines
        .Where(r => r.WeekDay == weekDay && r.Teacher.Id == teacherId)
        .Any(r => DoesOverlap(r.StartTime, r.EndTime, startTime, endTime));
    }
    public bool IsTeacherFree(long teacherId, TimeSpan startTime, TimeSpan endTime, DayOfWeek weekDay, long routineId)
    {
      return !_dbContext.ClassRoutines
        .Where(r => r.WeekDay == weekDay && r.Teacher.Id == teacherId && r.Id != routineId)
        .Any(r => DoesOverlap(r.StartTime, r.EndTime, startTime, endTime));
    }
    public bool IsSectionFree(long sectionId, TimeSpan startTime, TimeSpan endTime, DayOfWeek weekDay)
    {
      return !_dbContext.ClassRoutines
        .Where(r => r.WeekDay == weekDay && r.Section.Id == sectionId)
        .Any(r => DoesOverlap(r.StartTime, r.EndTime, startTime, endTime));
    }
    public bool IsSectionFree(long sectionId, TimeSpan startTime, TimeSpan endTime, DayOfWeek weekDay, long routineId)
    {
      return !_dbContext.ClassRoutines
        .Where(r => r.WeekDay == weekDay && r.Section.Id == sectionId && r.Id != routineId)
        .Any(r => DoesOverlap(r.StartTime, r.EndTime, startTime, endTime));
    }
    public bool IsRoomFree(long roomId, TimeSpan startTime, TimeSpan endTime, DayOfWeek weekDay)
    {
      return !_dbContext.ClassRoutines
        .Where(r => r.WeekDay == weekDay && r.Room.Id == roomId)
        .Any(r => DoesOverlap(r.StartTime, r.EndTime, startTime, endTime));
    }

    public bool IsRoomFree(long roomId, TimeSpan startTime, TimeSpan endTime, DayOfWeek weekDay, long routineId)
    {
      return !_dbContext.ClassRoutines
        .Where(r => r.WeekDay == weekDay && r.Room.Id == roomId && r.Id != routineId)
        .Any(r => DoesOverlap(r.StartTime, r.EndTime, startTime, endTime));
    }

    public bool IsTimeOverlapsWithBreakTime(TimeSpan startTime, TimeSpan endTime, long sectionId)
    {
      var branchMedium = _dbContext.Sections
        .Include(nameof(BranchMedium))
        .FirstOrDefault(s => s.Id == sectionId)?
        .BranchMedium;

      var breakTime = _dbContext.BreakTimes.FirstOrDefault(x => x.BranchMedium.Id == branchMedium.Id);// branchMedium.Sections.FirstOrDefault(x => x.BranchMedium.Id == branchMedium.Id).BreakTime;
      return branchMedium != null && breakTime != null &&
             !DoesOverlap(breakTime.StartTime, breakTime.EndTime, startTime, endTime);
    }

    public bool IsBreakTimeExist(long sectionId)
    {
      var branchMedium = _dbContext.Sections
        .Include(nameof(BranchMedium))
        .FirstOrDefault(s => s.Id == sectionId)?
        .BranchMedium;

      var breakTime = _dbContext.BreakTimes.FirstOrDefault(x => x.BranchMedium.Id == branchMedium.Id);// branchMedium.Sections.FirstOrDefault(x => x.BranchMedium.Id == branchMedium.Id).BreakTime;
      return breakTime != null;
    }

    public bool IsAllTheClassesOfTheDayAdded(DayOfWeek weekDay, long sectionId)
    {
      var section = _dbContext.Sections.FirstOrDefault(s => s.Id == sectionId);
      return section != null && _dbContext.ClassRoutines.Count(r => r.WeekDay == weekDay && r.Section.Id == sectionId) <
             section.TotalClasses;
    }

    public bool IsAllTheClassesOfTheDayAdded(DayOfWeek weekDay, long sectionId, long routineId)
    {
      var section = _dbContext.Sections.FirstOrDefault(s => s.Id == sectionId);
      return section != null && _dbContext.ClassRoutines.Count(r => r.WeekDay == weekDay && r.Section.Id == sectionId && r.Id != routineId) <
             section.TotalClasses;
    }

    public bool IsTimeRangeMatchesWithClassDuration(TimeSpan startTime, TimeSpan endTime, long sectionId)
    {
      var section = _dbContext.Sections.FirstOrDefault(s => s.Id == sectionId);
      return section != null && section.DurationOfClass == GetTimeRangeMinutes(startTime, endTime);
    }

    public bool IsSubjectAlreadyAdded(DayOfWeek weekDay, long subjectId, long sectionId)
    {
      return !_dbContext.ClassRoutines.Any(r =>
        r.Section.Id == sectionId && r.WeekDay == weekDay && r.Subject.Id == subjectId);
    }

    public bool IsSubjectAlreadyAdded(DayOfWeek weekDay, long subjectId, long sectionId, long routineId)
    {
      return !_dbContext.ClassRoutines.Any(r =>
        r.Section.Id == sectionId && r.WeekDay == weekDay && r.Subject.Id == subjectId && r.Id != routineId);
    }

    public bool IsRoomCapacityValid(long roomId, long sectionId)
    {
      var room = _dbContext.Rooms.FirstOrDefault(r => r.Id == roomId);
      var section = _dbContext.Sections.FirstOrDefault(s => s.Id == sectionId);
      return room != null && section != null && room.ClassTimeCapacity >= section.NumberOfStudents;
    }

    public bool IsClassStartTimeBetweenShiftPeriod(TimeSpan startTime, long sectionId)
    {
      var section = _dbContext.Sections
        .Include(nameof(BranchMedium))
        .Include(nameof(BranchMedium) + "." + nameof(Shift))
        .FirstOrDefault(s => s.Id == sectionId);
      return section != null
             && section.BranchMedium.Shift.StartTime <= startTime
             && section.BranchMedium.Shift.EndTime > startTime;
    }

    public bool IsClassEndTimeBetweenShiftPeriod(TimeSpan endTime, long sectionId)
    {
      var section = _dbContext.Sections
        .Include(nameof(BranchMedium))
        .Include(nameof(BranchMedium) + "." + nameof(Shift))
        .FirstOrDefault(s => s.Id == sectionId);
      return section != null
             && section.BranchMedium.Shift.EndTime >= endTime
             && section.BranchMedium.Shift.StartTime < endTime;
    }

    private int GetTimeRangeMinutes(TimeSpan startTime, TimeSpan endTime)
    {
      var hour = Math.Abs((endTime - startTime).Hours);
      var minutes = Math.Abs((endTime - startTime).Minutes);
      return hour * 60 + minutes;
    }

    private bool DoesOverlap(TimeSpan startTime1, TimeSpan endTime1, TimeSpan startTime2, TimeSpan endTime2)
    {
      if (startTime1 == startTime2 && endTime1 == endTime2)
        return Overlaps;
      return _timeOverlappingChecker.DoesOverlap(startTime1, endTime1, startTime2, endTime2);
    }
  }
}