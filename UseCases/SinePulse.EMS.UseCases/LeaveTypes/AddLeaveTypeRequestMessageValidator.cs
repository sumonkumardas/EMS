using FluentValidation;
using SinePulse.EMS.Messages.LeaveTypeMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.LeaveTypes
{
  public class AddLeaveTypeRequestMessageValidator : AbstractValidator<AddLeaveTypeRequestMessage>
  {
    private readonly IUniqueLeaveTypePropertyChecker _uniqueLeaveTypePropertyChecker;

    public AddLeaveTypeRequestMessageValidator(IUniqueLeaveTypePropertyChecker uniqueLeaveTypePropertyChecker)
    {
      _uniqueLeaveTypePropertyChecker = uniqueLeaveTypePropertyChecker;
      RuleFor(x => x.LeaveName).NotEmpty().WithMessage("Please Specify Leave Name.");
      RuleFor(x => x.LeaveName).NotNull().WithMessage("Please Specify Leave Name.");
      RuleFor(x => x.LeaveName).MaximumLength(250).WithMessage("Leave Name is too long");
      RuleFor(x => x.LeaveName).Must(IsUniqueLeaveName).WithMessage("Leave Name already exists");

      RuleFor(x => x.LeavesPerYear).LessThan(365).WithMessage("Number of Leaves Per Year, must be less than 365");
      RuleFor(x => x.LeavesPerYear).GreaterThan(0).WithMessage("Number of Leaves must be greater than 0.");

      RuleFor(x => x.PercentageOfLeaveCarriedForward).GreaterThanOrEqualTo(1)                  
        .WithMessage("Percentage Of Leave Carried Forward must be greater than 0.");
      RuleFor(x => x.PercentageOfLeaveCarriedForward).LessThanOrEqualTo(100)
        .WithMessage("Percentage Of Leave Forward Carried can no be greater than 100.");
      
      RuleFor(x => x.PercentageOfLeaveCarriedForward).Must(IsPercentageOfLeaveCarriedForwardValueEntered)
        .WithMessage("Enter Percentage Of Leave Carried Forward.");
    }

    private bool IsPercentageOfLeaveCarriedForwardValueEntered(AddLeaveTypeRequestMessage addLeaveTypeRequestMessage,
      int? percentageOfLeaveCarriedForward)
    {
      if (addLeaveTypeRequestMessage.WillLeaveCarriedForward && percentageOfLeaveCarriedForward != null && percentageOfLeaveCarriedForward > 0)
        return true;
      if (!addLeaveTypeRequestMessage.WillLeaveCarriedForward)
        return true;
      return false;
    }

    private bool IsUniqueLeaveName(string leaveName)
    {
      return _uniqueLeaveTypePropertyChecker.IsUniqueLeaveName(leaveName);
    }
  }
}