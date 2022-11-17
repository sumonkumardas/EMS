using FluentValidation;
using SinePulse.EMS.Messages.EmployeeLeaveMessages;
using SinePulse.EMS.UseCases.Repositories;
using System;

namespace SinePulse.EMS.UseCases.EmployeeLeaves
{
  public class AddEmployeeLeaveRequestMessageValidator : AbstractValidator<AddEmployeeLeaveRequestMessage>
  {
    private readonly ILeaveAmountValidityChecker _leaveAmountValidityChecker;
    public AddEmployeeLeaveRequestMessageValidator(ILeaveAmountValidityChecker leaveAmountValidityChecker)
    {
      _leaveAmountValidityChecker = leaveAmountValidityChecker;
      RuleFor(x => x.Reason).NotEmpty().WithMessage("Please specify Reason");
      RuleFor(x => x.Reason).MaximumLength(200).WithMessage("Reason is too long.");
      RuleFor(x => x.Reason).NotNull().WithMessage("Please specify Reason");

      RuleFor(x => x.StartDate).NotEmpty().WithMessage("Please specify Start Date");
      RuleFor(x => x.StartDate).NotNull().WithMessage("Please specify Start Date");
      RuleFor(x => x.StartDate).GreaterThan(DateTime.Now).WithMessage("Start Date must be a future date.");

      RuleFor(x => x.EndDate).NotEmpty().WithMessage("Please specify End Date");
      RuleFor(x => x.EndDate).NotNull().WithMessage("Please specify End Date");
      RuleFor(x => x.EndDate).GreaterThan(DateTime.Now).WithMessage("End Date must be greater than current date.");
      RuleFor(x => x.EndDate).GreaterThanOrEqualTo(x => x.StartDate).WithMessage("End Date must be greater or same as Start date.");
      RuleFor(x => x.EndDate).Must(IsLeaveAmountValid).WithMessage("You Don't have enough leave left of this leave type.");

      RuleFor(x => x.EmployeeId).NotEmpty().WithMessage("Please specify Employee");
      RuleFor(x => x.EmployeeId).NotNull().WithMessage("Please specify Employee");
    }

    private bool IsLeaveAmountValid(AddEmployeeLeaveRequestMessage message,DateTime endDate)
    {
      return _leaveAmountValidityChecker.IsLeaveAmountValid(message.EmployeeId, message.LeaveTypeId, message.StartDate, message.EndDate);
    }
  }
}