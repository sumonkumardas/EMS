using FluentValidation.Results;

namespace SinePulse.EMS.Messages.InstituteMessages
{
  public class EditInstituteResponseMessage
  {
    public ValidationResult ValidationResult { get; }

    public EditInstituteResponseMessage(ValidationResult validationResult)
    {
      ValidationResult = validationResult;
    }
  }
}