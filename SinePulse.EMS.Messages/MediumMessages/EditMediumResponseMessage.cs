using FluentValidation.Results;

namespace SinePulse.EMS.Messages.MediumMessages
{
  public class EditMediumResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    
    public EditMediumResponseMessage(ValidationResult validationResult)
    {
      ValidationResult = validationResult;
    }
  }
}