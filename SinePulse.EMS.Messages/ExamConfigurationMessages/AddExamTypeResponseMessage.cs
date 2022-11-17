using FluentValidation.Results;

namespace SinePulse.EMS.Messages.ExamConfigurationMessages
{
  public class AddExamConfigurationResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public long ExamConfigurationId { get; }

    public AddExamConfigurationResponseMessage(ValidationResult validationResult)
    {
      ValidationResult = validationResult;
    }

    public AddExamConfigurationResponseMessage(ValidationResult validationResult, long examConfigurationId)
    {
      ValidationResult = validationResult;
      ExamConfigurationId = examConfigurationId;
    }
  }
}