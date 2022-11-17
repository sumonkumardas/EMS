using FluentValidation;
using SinePulse.EMS.Messages.MarkMessages;

namespace SinePulse.EMS.UseCases.Marks
{
  public class AddMarkRequestMessageValidator : AbstractValidator<AddMarkRequestMessage>
  {
    public AddMarkRequestMessageValidator()
    {
      RuleFor(x => x.Comment).Must((m, x) => HasCommentIfGraceMarkIsGiven(x, m.GraceMark))
        .WithMessage("Remarks is required for Grace ClassTestMark");
    }

    private bool HasCommentIfGraceMarkIsGiven(string comment, decimal graceMark)
    {
      return graceMark == 0 || !string.IsNullOrWhiteSpace(comment);
    }
  }
}