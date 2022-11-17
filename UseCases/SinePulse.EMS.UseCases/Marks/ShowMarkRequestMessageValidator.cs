using FluentValidation;
using SinePulse.EMS.Messages.MarkMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.Marks
{
  public class ShowMarkRequestMessageValidator : AbstractValidator<ShowMarkRequestMessage>
  {
    private readonly IValidMarkChecker _validMarkChecker;

    public ShowMarkRequestMessageValidator(IValidMarkChecker validMarkChecker)
    {
      _validMarkChecker = validMarkChecker;

      RuleFor(x => x.MarkId).Must(IsValidMark).WithMessage("This mark doesn't exists");
    }

    private bool IsValidMark(long termId)
    {
      return _validMarkChecker.IsValidMark(termId);
    }
  }
}