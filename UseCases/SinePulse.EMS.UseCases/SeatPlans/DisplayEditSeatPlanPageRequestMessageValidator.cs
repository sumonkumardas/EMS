using FluentValidation;
using SinePulse.EMS.Messages.SeatPlanMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.SeatPlans
{
  public class DisplayEditSeatPlanPageRequestMessageValidator : AbstractValidator<DisplayEditSeatPlanPageRequestMessage>
  {
    private readonly IValidSeatPlanChecker _validSeatPlanChecker;

    public DisplayEditSeatPlanPageRequestMessageValidator(IValidSeatPlanChecker validSeatPlanChecker)
    {
      _validSeatPlanChecker = validSeatPlanChecker;
      RuleFor(x => x.SeatPlanId).Must(IsValidSeatPlan).WithMessage("Invalid Term Test");
    }

    private bool IsValidSeatPlan(long seatPlanId)
    {
      return _validSeatPlanChecker.IsValidSeatPlan(seatPlanId);
    }
  }
}