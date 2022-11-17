using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using SinePulse.EMS.Messages.SeatPlanMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.SeatPlans
{
  public class AddSeatPlanRequestMessageValidator : AbstractValidator<AddSeatPlanRequestMessage>
  {
    private readonly IAddSeatPlanValidationHelper _addSeatPlanValidationHelper;

    public AddSeatPlanRequestMessageValidator(IAddSeatPlanValidationHelper addSeatPlanValidationHelper)
    {
      _addSeatPlanValidationHelper = addSeatPlanValidationHelper;

      RuleFor(x => x.TotalStudent).GreaterThanOrEqualTo(1).WithMessage(
        "Please Specify a positive value for TotalStudent");
      RuleFor(x => x.TotalStudent).MustAsync(CanAddStudentFromTermTest).WithMessage(
        "Test does not have this amount of Student");
      RuleFor(x => x.TotalStudent).MustAsync(CanAddStudentInExamRoom).WithMessage(
        "Selected room does not have this amount of empty seat");
      RuleFor(x => x.RollRange).NotEmpty().WithMessage(
        "Please specify a Roll Range of Students");
    }

    private async Task<bool> CanAddStudentFromTermTest(AddSeatPlanRequestMessage message, int totalStudent,
      CancellationToken cancellationToken)
    {
      return await _addSeatPlanValidationHelper.CanAddStudentFromTermTest(message.TestId, totalStudent);
    }

    private async Task<bool> CanAddStudentInExamRoom(AddSeatPlanRequestMessage message, int totalStudent,
      CancellationToken cancellationToken)
    {
      return await _addSeatPlanValidationHelper.CanAddStudentInExamRoom(message.RoomId, totalStudent);
    }
  }
}