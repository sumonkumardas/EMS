using FluentValidation.Results;

namespace SinePulse.EMS.Messages.TermMessages
{
  public class AddTermResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public long TermId { get; }

    public AddTermResponseMessage(ValidationResult validationResult)
    {
      ValidationResult = validationResult;
    }

    public AddTermResponseMessage(ValidationResult validationResult, long termId)
    {
      ValidationResult = validationResult;
      TermId = termId;
    }
  }
}