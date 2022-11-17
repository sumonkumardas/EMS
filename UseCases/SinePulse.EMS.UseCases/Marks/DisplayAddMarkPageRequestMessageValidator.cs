using FluentValidation;
using SinePulse.EMS.Messages.MarkMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.Marks
{
  public class DisplayAddMarkPageRequestMessageValidator : AbstractValidator<DisplayAddMarkPageRequestMessage>
  {
    private readonly IValidTermTestChecker _validTermTestChecker;

    public DisplayAddMarkPageRequestMessageValidator(IValidTermTestChecker validTermTestChecker)
    {
//      _validTermTestChecker = validTermTestChecker;
//      RuleFor(x => x.TestId).Must(IsValidTermTest).WithMessage("Invalid Term Test");
    }

    private bool IsValidTermTest(long testId)
    {
      return _validTermTestChecker.IsValidTermTest(testId);
    }
  }
}