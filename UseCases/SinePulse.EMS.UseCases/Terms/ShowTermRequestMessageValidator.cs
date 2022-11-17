using FluentValidation;
using SinePulse.EMS.Messages.TermMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.Terms
{
  public class ShowTermRequestMessageValidator : AbstractValidator<ShowTermRequestMessage>
  {
    private readonly IValidTermChecker _validTermChecker;

    public ShowTermRequestMessageValidator(IValidTermChecker validTermChecker)
    {
      _validTermChecker = validTermChecker;

      RuleFor(x => x.TermId).Must(IsValidTerm).WithMessage("This term doesn't exists");
    }

    private bool IsValidTerm(long termId)
    {
      return _validTermChecker.IsValidTerm(termId);
    }
  }
}