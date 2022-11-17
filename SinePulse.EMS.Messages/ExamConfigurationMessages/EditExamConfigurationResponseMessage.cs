using FluentValidation.Results;

namespace SinePulse.EMS.Messages.ExamConfigurationMessages
{
  public class EditExamConfigurationResponseMessage
  {
    public ValidationResult ValidationResult { get; }

    public EditExamConfigurationResponseMessage(ValidationResult validationResult)
    {
      ValidationResult = validationResult;
    }
  }
}