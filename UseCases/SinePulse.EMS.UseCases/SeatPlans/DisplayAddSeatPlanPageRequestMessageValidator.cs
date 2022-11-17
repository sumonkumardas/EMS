using FluentValidation;
using SinePulse.EMS.Messages.SeatPlanMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.SeatPlans
{
  public class DisplayAddSeatPlanPageRequestMessageValidator : AbstractValidator<DisplayAddSeatPlanPageRequestMessage>
  {
    private readonly IValidTermTestChecker _validTermTestChecker;

    public DisplayAddSeatPlanPageRequestMessageValidator(IValidTermTestChecker validTermTestChecker)
    {
      _validTermTestChecker = validTermTestChecker;
      RuleFor(x => x.TestId).Must(IsValidTermTest).WithMessage("Invalid Term Test");
    }

    private bool IsValidTermTest(long testId)
    {
      return _validTermTestChecker.IsValidTermTest(testId);
    }
  }
}