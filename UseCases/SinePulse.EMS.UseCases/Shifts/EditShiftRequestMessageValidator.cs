using System;
using FluentValidation;
using SinePulse.EMS.Messages.ShiftMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.Shifts
{
  public class EditShiftRequestMessageValidator : AbstractValidator<EditShiftRequestMessage>
  {
    private readonly IUniqueShiftPropertyChecker _uniqueShiftPropertyChecker;

    public EditShiftRequestMessageValidator(IUniqueShiftPropertyChecker uniqueShiftPropertyChecker)
    {
      _uniqueShiftPropertyChecker = uniqueShiftPropertyChecker;
      RuleFor(x => x.ShiftName).NotEmpty().WithMessage("Please specify Shift name");
      RuleFor(x => x.ShiftName).MaximumLength(200).WithMessage("Shift name is too long");
      RuleFor(x => x.ShiftName).NotNull().WithMessage("Please specify Shift name");
      RuleFor(x => x.ShiftName).Must((m, x) => IsUniqueShiftName(x, m.BranchId, m.ShiftId))
        .WithMessage("Shift Name already exists.");

      RuleFor(x => x.StartTime).NotNull().WithMessage("Please specify Shift Start time.");
      RuleFor(x => x.StartTime).NotEmpty().WithMessage("Please specify Shift Start time.");

      RuleFor(x => x.EndTime).NotNull().WithMessage("Please specify Shift End time.");
      RuleFor(x => x.EndTime).NotEmpty().WithMessage("Please specify Shift End time.");
      RuleFor(x => x.EndTime).GreaterThan(x => x.StartTime).WithMessage("End Time Must be Greater than Start Time");
      RuleFor(x => x).Must((m, x) => IsShiftStartAndEndTimeUnique(m.StartTime, m.EndTime, m.BranchId, m.ShiftId))
        .WithMessage("A Shift with same Start & End time already exists in this Branch.");
      RuleFor(x => x.Status).IsInEnum().WithMessage("Select Status");
    }

    private bool IsShiftStartAndEndTimeUnique(TimeSpan startTime, TimeSpan endTime, long branchId, long shiftId)
    {
      return _uniqueShiftPropertyChecker.IsUniqueShiftTime(startTime, endTime, branchId, shiftId);
    }


    private bool IsUniqueShiftName(string shiftName, long branchId, long shiftId)
    {
      return _uniqueShiftPropertyChecker.IsUniqueShiftName(shiftName, branchId, shiftId);
    }
  }
}