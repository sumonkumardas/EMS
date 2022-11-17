using System;

namespace SinePulse.EMS.UseCases.Repositories
{
  public interface IClassRoutineOverlappingChecker
  {
    bool IsTeacherFree(long teacherId, TimeSpan startTime, TimeSpan endTime, DayOfWeek weekDay);
    bool IsSectionFree(long sectionId, TimeSpan startTime, TimeSpan endTime, DayOfWeek weekDay);
    bool IsRoomFree(long roomId, TimeSpan startTime, TimeSpan endTime, DayOfWeek weekDay);
    bool IsTeacherFree(long teacherId, TimeSpan startTime, TimeSpan endTime, DayOfWeek weekDay,long routineId);
    bool IsSectionFree(long sectionId, TimeSpan startTime, TimeSpan endTime, DayOfWeek weekDay,long routineId);
    bool IsRoomFree(long roomId, TimeSpan startTime, TimeSpan endTime, DayOfWeek weekDay,long routineId);
    bool IsTimeOverlapsWithBreakTime(TimeSpan startTime, TimeSpan endTime, long sectionId);
    bool IsBreakTimeExist(long sectionId);
    bool IsAllTheClassesOfTheDayAdded(DayOfWeek weekDay, long sectionId);
    bool IsAllTheClassesOfTheDayAdded(DayOfWeek weekDay, long sectionId,long routineId);
    bool IsTimeRangeMatchesWithClassDuration(TimeSpan startTime, TimeSpan endTime, long sectionId);
    bool IsSubjectAlreadyAdded(DayOfWeek weekDay, long subjectId, long sectionId);
    bool IsSubjectAlreadyAdded(DayOfWeek weekDay, long subjectId, long sectionId,long routineId);
    bool IsRoomCapacityValid(long roomId, long sectionId);
    bool IsClassStartTimeBetweenShiftPeriod(TimeSpan startTime, long sectionId);
    bool IsClassEndTimeBetweenShiftPeriod(TimeSpan endTime, long sectionId);
  }
}