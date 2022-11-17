using System;
using System.Collections.Generic;
using FluentValidation;
using SinePulse.EMS.Messages.BreakTimeMessages;
using SinePulse.EMS.ProjectPrimitives;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.BreakTimes
{
  public class AddBreakTimeRequestMessageValidator : AbstractValidator<AddBreakTimeRequestMessage>
  {
    private readonly IUniqueBreakTimePropertyChecker _uniqueBreakTimePropertyChecker;
    public AddBreakTimeRequestMessageValidator(IUniqueBreakTimePropertyChecker uniqueBreakTimePropertyChecker)
    {
      _uniqueBreakTimePropertyChecker = uniqueBreakTimePropertyChecker;
      RuleFor(x => x.StartTime).NotEmpty().WithMessage("Please Specify Start Time.");
      RuleFor(x => x.StartTime).NotNull().WithMessage("Please Specify Start Time.");
      RuleFor(x => x.EndTime).NotEmpty().WithMessage("Please Specify End Time.");
      RuleFor(x => x.EndTime).NotNull().WithMessage("Please Specify End Time.");
      RuleFor(x => x.StartTime).LessThan(x => x.EndTime).WithMessage("Start Time must be prior than End Time.");
      
      RuleFor(x => x).Must((m, x) => IsBreakTimeBetweenShiftTime(m.StartTime, m.EndTime, m.BranchMediumId))
        .WithMessage("Break Time must be in between Shift Time period.");
    }

    private bool IsBreakTimeOverlaps(TimeSpan startTime, TimeSpan endTime, IEnumerable<WeekDays> weekDays, long branchMediumId)
    {
      return _uniqueBreakTimePropertyChecker.IsBreakTimeOverlaps(startTime, endTime, weekDays, branchMediumId);
    }

    private bool IsBreakTimeBetweenShiftTime(TimeSpan startTime, TimeSpan endTime, long branchMediumId)
    {
      return _uniqueBreakTimePropertyChecker.IsBreakTimeBetweenShiftTime(startTime, endTime, branchMediumId);
    }

    private bool IsBreakTimeExists(TimeSpan startTime, TimeSpan endTime, IEnumerable<WeekDays> weekDays,
      long branchMediumId)
    {
      return _uniqueBreakTimePropertyChecker.IsUniqueBreakTime(startTime, endTime, weekDays, branchMediumId);
    }
  }
}