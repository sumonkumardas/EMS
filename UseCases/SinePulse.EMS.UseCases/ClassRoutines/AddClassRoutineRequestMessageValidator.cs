using System;
using FluentValidation;
using SinePulse.EMS.Messages.ClassRoutineMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.ClassRoutines
{
  public class AddClassRoutineRequestMessageValidator : AbstractValidator<AddClassRoutineRequestMessage>
  {
    private readonly IClassRoutineOverlappingChecker _classRoutineOverlappingChecker;

    public AddClassRoutineRequestMessageValidator(IClassRoutineOverlappingChecker classRoutineOverlappingChecker)
    {
      _classRoutineOverlappingChecker = classRoutineOverlappingChecker;
      RuleFor(x => x).Must(x => IsBreakTimeExist(x.SectionId))
        .WithMessage("Break time not configured.");
      RuleFor(x => x.EndTime).Must((m, x) => IsTimeRangeIsCorrect(m.StartTime, m.EndTime))
        .WithMessage("Must be a valid class time");
      RuleFor(x => x.TeacherId).Must((m, x) => IsTeacherFree(m.TeacherId, m.StartTime, m.EndTime, m.WeekDay))
        .WithMessage("Teacher is assigned in another class in this time period");
      RuleFor(x => x.SectionId).Must((m, x) => IsSectionFree(m.SectionId, m.StartTime, m.EndTime, m.WeekDay))
        .WithMessage("Section is assigned in another class in this time period");
      RuleFor(x => x.RoomId).Must((m, x) => IsRoomFree(m.RoomId, m.StartTime, m.EndTime, m.WeekDay))
        .WithMessage("Room is assigned in another class in this time period");
      RuleFor(x => x).Must((m, x) => IsTimeOverlapsWithBreakTime(m.StartTime, m.EndTime, m.SectionId))
        .WithMessage("Entered Class Time Overlaps with Break Time.");
      RuleFor(x => x.WeekDay).Must((m, x) => IsAllTheClassesOfTheDayAdded(m.WeekDay, m.SectionId))
        .WithMessage(x => $"All the Classes of {x.WeekDay:G} already added.");
      RuleFor(x => x).Must((m, x) => IsTimeRangeMatchesWithClassDuration(m.StartTime, m.EndTime, m.SectionId))
        .WithMessage("Entered Class Time Range should be same as Class Duration.");
      RuleFor(x => x.SubjectId).Must((m, x) => IsSubjectAlreadyAdded(m.WeekDay, m.SubjectId, m.SectionId))
        .WithMessage(x => $"Subject Already Added on {x.WeekDay:G}.");
      RuleFor(x => x.RoomId).Must((m, x) => IsRoomCapacityValid(m.RoomId, m.SectionId))
        .WithMessage("Room's class time capacity is less than sections total students.");
      RuleFor(x => x.StartTime).Must((m, x) => IsClassStartTimeBetweenShiftPeriod(m.StartTime, m.SectionId))
        .WithMessage("Class Start time must be between start and end time of Shift");
      RuleFor(x => x.EndTime).Must((m, x) => IsClassEndTimeBetweenShiftPeriod(m.EndTime, m.SectionId))
        .WithMessage("Class End time must be between start and end time of Shift");

    }

    private bool IsClassEndTimeBetweenShiftPeriod(TimeSpan endTime, long sectionId)
    {
      return _classRoutineOverlappingChecker.IsClassEndTimeBetweenShiftPeriod(endTime, sectionId);
    }

    private bool IsClassStartTimeBetweenShiftPeriod(TimeSpan startTime, long sectionId)
    {
      return _classRoutineOverlappingChecker.IsClassStartTimeBetweenShiftPeriod(startTime, sectionId);
    }

    private bool IsRoomCapacityValid(long roomId, long sectionId)
    {
      return _classRoutineOverlappingChecker.IsRoomCapacityValid(roomId, sectionId);
    }

    private bool IsSubjectAlreadyAdded(DayOfWeek weekDay, long subjectId, long sectionId)
    {
      return _classRoutineOverlappingChecker.IsSubjectAlreadyAdded(weekDay, subjectId, sectionId);
    }

    private bool IsTimeRangeMatchesWithClassDuration(TimeSpan startTime, TimeSpan endTime, long sectionId)
    {
      return _classRoutineOverlappingChecker.IsTimeRangeMatchesWithClassDuration(startTime, endTime, sectionId);
    }

    private bool IsAllTheClassesOfTheDayAdded(DayOfWeek weekDay, long sectionId)
    {
      return _classRoutineOverlappingChecker.IsAllTheClassesOfTheDayAdded(weekDay, sectionId);
    }

    private bool IsTimeOverlapsWithBreakTime(TimeSpan startTime, TimeSpan endTime, long sectionId)
    {
      return _classRoutineOverlappingChecker.IsTimeOverlapsWithBreakTime(startTime, endTime, sectionId);
    }

    private bool IsBreakTimeExist(long sectionId)
    {
      return _classRoutineOverlappingChecker.IsBreakTimeExist(sectionId);
    }

    private bool IsTeacherFree(long teacherId, TimeSpan startTime, TimeSpan endTime, DayOfWeek weekDay)
    {
      return _classRoutineOverlappingChecker.IsTeacherFree(teacherId, startTime, endTime, weekDay);
    }

    private bool IsSectionFree(long sectionId, TimeSpan startTime, TimeSpan endTime, DayOfWeek weekDay)
    {
      return _classRoutineOverlappingChecker.IsSectionFree(sectionId, startTime, endTime, weekDay);
    }

    private bool IsRoomFree(long roomId, TimeSpan startTime, TimeSpan endTime, DayOfWeek weekDay)
    {
      return _classRoutineOverlappingChecker.IsRoomFree(roomId, startTime, endTime, weekDay);
    }

    private bool IsTimeRangeIsCorrect(TimeSpan startTime, TimeSpan endTime)
    {
      return endTime > startTime;
    }
  }
}