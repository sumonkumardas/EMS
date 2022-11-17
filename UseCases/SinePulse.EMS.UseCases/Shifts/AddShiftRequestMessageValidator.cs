using System;
using FluentValidation;
using SinePulse.EMS.Messages.ShiftMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.Shifts
{
  public class AddShiftRequestMessageValidator : AbstractValidator<AddShiftRequestMessage>
  {
    private readonly IUniqueShiftPropertyChecker _uniqueShiftPropertyChecker;

    public AddShiftRequestMessageValidator(IUniqueShiftPropertyChecker shiftPropertyChecker)
    {
      _uniqueShiftPropertyChecker = shiftPropertyChecker;
      RuleFor(x => x.ShiftName).NotEmpty().WithMessage("Please specify Shift name");
      RuleFor(x => x.ShiftName).MaximumLength(200).WithMessage("Shift name is too long");
      RuleFor(x => x.ShiftName).NotNull().WithMessage("Please specify Shift name");
      RuleFor(x => x.ShiftName).Must((m, x) => IsUniqueShiftName(x, m.BranchId))
        .WithMessage("Shift name already exists.");
      RuleFor(x => x.StartTime).NotNull().WithMessage("Please specify Shift Start time.");
      RuleFor(x => x.StartTime).NotEmpty().WithMessage("Please specify Shift Start time.");
      RuleFor(x => x.EndTime).NotNull().WithMessage("Please specify Shift End time.");
      RuleFor(x => x.EndTime).NotEmpty().WithMessage("Please specify Shift End time.");
      RuleFor(x => x.EndTime).GreaterThan(x => x.StartTime).WithMessage("End Time Must be Greater than Start Time");
      RuleFor(x => x).Must((m, x) => IsShiftStartAndEndTimeUnique(m.StartTime, m.EndTime, m.BranchId))
        .WithMessage("A Shift with same Start & End time already exists in this Branch.");
    }

    private bool IsShiftStartAndEndTimeUnique(TimeSpan startTime, TimeSpan endTime, long branchId)
    {
      return _uniqueShiftPropertyChecker.IsShiftStartAndEndTimeUnique(startTime, endTime, branchId);
    }

    private bool IsUniqueShiftName(string shiftName, long branchId)
    {
      return _uniqueShiftPropertyChecker.IsUniqueShiftName(shiftName, branchId);
    }
  }
}