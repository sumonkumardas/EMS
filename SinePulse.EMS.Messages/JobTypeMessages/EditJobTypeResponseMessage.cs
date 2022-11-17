using FluentValidation.Results;

namespace SinePulse.EMS.Messages.JobTypeMessages
{
  public class EditJobTypeResponseMessage
  {
    public ValidationResult ValidationResult { get; }

    public EditJobTypeResponseMessage(ValidationResult validationResult)
    {
      ValidationResult = validationResult;
    }
  }
}