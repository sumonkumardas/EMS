using FluentValidation;
using SinePulse.EMS.Messages.WorkingShiftMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.WorkingShifts
{
  public class AddWorkingShiftRequestMessageValidator : AbstractValidator<AddWorkingShiftRequestMessage>
  {
    private readonly IUniqueWorkingShiftPropertyChecker _uniqueWorkingShiftPropertyChecker;
    public AddWorkingShiftRequestMessageValidator(IUniqueWorkingShiftPropertyChecker uniqueWorkingShiftPropertyChecker)
    {
      _uniqueWorkingShiftPropertyChecker = uniqueWorkingShiftPropertyChecker;
      RuleFor(x => x.ShiftName).NotEmpty().WithMessage("Please specify Shift name");
      RuleFor(x => x.ShiftName).MaximumLength(200).WithMessage("Shift name is too long");
      RuleFor(x => x.ShiftName).NotNull().WithMessage("Please specify Shift name");
      RuleFor(x => x.ShiftName).Must(IsUniqueWorkingShiftName).WithMessage("Shift name already exists.");
      RuleFor(x => x.StartTime).NotNull().WithMessage("Please specify Shift Start time.");
      RuleFor(x => x.StartTime).NotEmpty().WithMessage("Please specify Shift Start time.");
      RuleFor(x => x.EndTime).NotNull().WithMessage("Please specify Shift End time.");
      RuleFor(x => x.EndTime).NotEmpty().WithMessage("Please specify Shift End time.");
      RuleFor(x => x.EndTime).GreaterThan(x => x.StartTime).WithMessage("End Time Must be Greater than Start Time");
    }

    private bool IsUniqueWorkingShiftName(string shiftName)
    {
      return _uniqueWorkingShiftPropertyChecker.IsUniqueWorkingShiftName(shiftName);
    }
  }
}