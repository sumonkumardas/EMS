using FluentValidation;
using SinePulse.EMS.Messages.ClassTestMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.ClassTests
{
  public class ShowClassTestRequestMessageValidator : AbstractValidator<ShowClassTestRequestMessage>
  {
    private readonly IValidClassTestChecker _validClassTestChecker;

    public ShowClassTestRequestMessageValidator(IValidClassTestChecker validClassTestChecker)
    {
      _validClassTestChecker = validClassTestChecker;

      RuleFor(x => x.ClassTestId).Must(IsValidClassTest).WithMessage("This Class ClassTest doesn't exists");
    }

    private bool IsValidClassTest(long classTestId)
    {
      return _validClassTestChecker.IsValidClassTest(classTestId);
    }
  }
}