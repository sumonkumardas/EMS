using System;
using FluentValidation;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Messages.TermTestMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.TermTests
{
  public class EditTermTestRequestMessageValidator : AbstractValidator<EditTermTestRequestMessage>
  {
    private readonly ITermTestOverlappingChecker _termTestOverlappingChecker;

    public EditTermTestRequestMessageValidator(ITermTestOverlappingChecker termTestOverlappingChecker)
    {
      _termTestOverlappingChecker = termTestOverlappingChecker;
      RuleFor(x => x.Date).Must((m, x) => IsOnBetweenValidTermDate(x, m.TermId))
        .WithMessage("Date must be in between term start date and end date.");
      RuleFor(x => x.StartTime).Must((m, x) => IsNonOverlappingStartTime(m.Date, m.StartTime, m.TermId, m.ClassId,
          (GroupEnum) m.GroupId, m.SubjectId, m.ExamType,m.Id))
        .WithMessage(x => "Another ClassTest exists for same Class with in this start time period.");
      RuleFor(x => x.EndTime).Must((m, x) => IsNonOverlappingEndTime(m.Date, m.EndTime, m.TermId, m.ClassId,
          (GroupEnum) m.GroupId, m.SubjectId, m.ExamType,m.Id))
        .WithMessage(x => "Another ClassTest exists for same Class with in this end time period.");

      RuleFor(x => x.SubjectId).Must((m, x) =>
          IsTermTestForSubjectAlreadyScheduled(m.ClassId, m.SubjectId, m.TermId, m.ExamType,m.Id))
        .WithMessage("Exam for this subject already scheduled.");
    }

    private bool IsTermTestForSubjectAlreadyScheduled(long classId, long subjectId, long termId,
      ExamTypeEnum examType,long termTestId)
    {
      return _termTestOverlappingChecker.IsTermTestForSubjectAlreadyScheduled(classId, subjectId, termId, examType,termTestId);
    }

    private bool IsNonOverlappingStartTime(DateTime routineDate, TimeSpan startTime, long termId, long classId,
      GroupEnum groupId, long subjectId, ExamTypeEnum examType,long termTestId)
    {
      return !_termTestOverlappingChecker.IsNonOverlappingStartTime(routineDate, startTime, termId, classId, groupId,
        subjectId,termTestId);
    }

    private bool IsNonOverlappingEndTime(DateTime routineDate, TimeSpan endTime, long termId, long classId,
      GroupEnum groupId, long subjectId, ExamTypeEnum examType,long termTestId)
    {
      return !_termTestOverlappingChecker.IsNonOverlappingEndTime(routineDate, endTime, termId, classId, groupId,
        subjectId,termTestId);
    }

    private bool IsOnBetweenValidTermDate(DateTime routineDate, long termId)
    {
      return _termTestOverlappingChecker.IsOnBetweenValidTermDate(routineDate, termId);
    }
  }
}