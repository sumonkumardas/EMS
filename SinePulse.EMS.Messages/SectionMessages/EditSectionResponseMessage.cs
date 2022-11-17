using FluentValidation.Results;

namespace SinePulse.EMS.Messages.SectionMessages
{
  public class EditSectionResponseMessage
  {
    public ValidationResult ValidationResult { get; }

    public EditSectionResponseMessage(ValidationResult validationResult)
    {
      ValidationResult = validationResult;
    }
  }
}