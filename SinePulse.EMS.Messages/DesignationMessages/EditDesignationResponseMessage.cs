using FluentValidation.Results;

namespace SinePulse.EMS.Messages.DesignationMessages
{
  public class EditDesignationResponseMessage
  {
    public ValidationResult ValidationResult { get; }

    public EditDesignationResponseMessage(ValidationResult validationResult)
    {
      ValidationResult = validationResult;
    }
  }
}