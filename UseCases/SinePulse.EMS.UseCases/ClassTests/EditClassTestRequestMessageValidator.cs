using FluentValidation;
using SinePulse.EMS.Messages.ClassTestMessages;
using SinePulse.EMS.UseCases.Repositories;
using System;

namespace SinePulse.EMS.UseCases.ClassTests
{
  public class EditClassTestRequestMessageValidator : AbstractValidator<EditClassTestRequestMessage>
  {
    private readonly IUniqueClassTestPropertyChecker _uniqueClassTestPropertyChecker;
    private readonly IClassTestOverlappingChecker _classTestOverlappingChecker;

    public EditClassTestRequestMessageValidator(IUniqueClassTestPropertyChecker uniqueClassTestPropertyChecker,
      IClassTestOverlappingChecker classTestOverlappingChecker)
    {
      _uniqueClassTestPropertyChecker = uniqueClassTestPropertyChecker;
      _classTestOverlappingChecker = classTestOverlappingChecker;
      RuleFor(x => x).Must(x => IsBreakTimeExist(x.SectionId))
        .WithMessage("Break time not configured.");
      RuleFor(x => x.TestName).NotEmpty().WithMessage("Please specify ClassTest Name.");
      RuleFor(x => x.TestName).NotNull().WithMessage("Please specify ClassTest Name.");
      RuleFor(x => x.TestName).Must((m, x) => IsUniqueClassTestName(x, m.SectionId, m.ExamConfigurationId,m.Id))
        .WithMessage("ClassTest Name already exists.");
      RuleFor(x => x.StartTime).Must((m, x) => IsClassStartTimeBetweenShiftPeriod(m.StartTime, m.SectionId))
        .WithMessage("Class Start time must be between start and end time of Shift");
      RuleFor(x => x.EndTime).Must((m, x) => IsClassEndTimeBetweenShiftPeriod(m.EndTime, m.SectionId))
        .WithMessage("Class End time must be between start and end time of Shift");
      RuleFor(x => x).Must(IsNonOverlappingClassTestPeriod)
        .WithMessage(x => "Another ClassTest for same Section exists with in this time period.");
      RuleFor(x => x).Must((m, x) => IsTimeOverlapsWithBreakTime(m.StartTime, m.EndTime, m.SectionId))
        .WithMessage("Entered ClassTest Time Overlaps with Break Time.");
    }

    private bool IsClassEndTimeBetweenShiftPeriod(TimeSpan endTime, long sectionId)
    {
      return _classTestOverlappingChecker.IsClassEndTimeBetweenShiftPeriod(endTime, sectionId);
    }

    private bool IsClassStartTimeBetweenShiftPeriod(TimeSpan startTime, long sectionId)
    {
      return _classTestOverlappingChecker.IsClassStartTimeBetweenShiftPeriod(startTime, sectionId);
    }
    private bool IsTimeOverlapsWithBreakTime(TimeSpan startTime, TimeSpan endTime, long sectionId)
    {
      return _classTestOverlappingChecker.IsTimeOverlapsWithBreakTime(startTime, endTime, sectionId);
    }
    private bool IsUniqueClassTestName(string classTestName, long sectionId, long examConfigurationId,long classTestId)
    {
      return _uniqueClassTestPropertyChecker.IsUniqueClassTestName(classTestName, sectionId, examConfigurationId,classTestId);
    }

    private bool IsNonOverlappingClassTestPeriod(EditClassTestRequestMessage test)
    {
      return _classTestOverlappingChecker.IsNonOverlappingClassTestPeriod(
        test.Date + test.StartTime, test.Date + test.EndTime, test.SectionId,test.Id);
    }

    private bool IsBreakTimeExist(long sectionId)
    {
      return _classTestOverlappingChecker.IsBreakTimeExist(sectionId);
    }
    
  }
}