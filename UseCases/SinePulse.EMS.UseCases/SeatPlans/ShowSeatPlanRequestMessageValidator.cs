using FluentValidation;
using SinePulse.EMS.Messages.SeatPlanMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.SeatPlans
{
  public class ShowSeatPlanRequestMessageValidator : AbstractValidator<ShowSeatPlanRequestMessage>
  {
    private readonly IValidSeatPlanChecker _validSeatPlanChecker;

    public ShowSeatPlanRequestMessageValidator(IValidSeatPlanChecker validSeatPlanChecker)
    {
      _validSeatPlanChecker = validSeatPlanChecker;

      RuleFor(x => x.SeatPlanId).Must(IsValidSeatPlan).WithMessage("This mark doesn't exists");
    }

    private bool IsValidSeatPlan(long termId)
    {
      return _validSeatPlanChecker.IsValidSeatPlan(termId);
    }
  }
}