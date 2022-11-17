using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using SinePulse.EMS.Messages.SeatPlanMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.SeatPlans
{
  public class EditSeatPlanRequestMessageValidator : AbstractValidator<EditSeatPlanRequestMessage>
  {
    private readonly IEditSeatPlanValidationHelper _addSeatPlanValidationHelper;

    public EditSeatPlanRequestMessageValidator(IEditSeatPlanValidationHelper addSeatPlanValidationHelper)
    {
      _addSeatPlanValidationHelper = addSeatPlanValidationHelper;

      RuleFor(x => x.TotalStudent).GreaterThanOrEqualTo(1).WithMessage(
        "Please Specify a positive value for TotalStudent");
      RuleFor(x => x.TotalStudent).MustAsync(CanEditStudentFromTermTest).WithMessage(
        "Test does not have this amount of Student");
      RuleFor(x => x.TotalStudent).MustAsync(CanEditStudentInExamRoom).WithMessage(
        "Selected room does not have this amount of empty seat");
      RuleFor(x => x.RollRange).NotEmpty().WithMessage(
        "Please specify a Roll Range of Students");
    }

    private async Task<bool> CanEditStudentFromTermTest(EditSeatPlanRequestMessage message, int totalStudent,
      CancellationToken cancellationToken)
    {
      return await _addSeatPlanValidationHelper.CanEditStudentFromSameTermTest(totalStudent, message.SeatPlanId);
    }

    private async Task<bool> CanEditStudentInExamRoom(EditSeatPlanRequestMessage message, int totalStudent,
      CancellationToken cancellationToken)
    {
      return await _addSeatPlanValidationHelper.CanEditStudentInExamRoom(message.RoomId, totalStudent,
        message.SeatPlanId);
    }
  }
}