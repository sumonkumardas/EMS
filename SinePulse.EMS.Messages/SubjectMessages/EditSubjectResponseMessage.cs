using FluentValidation.Results;

namespace SinePulse.EMS.Messages.SubjectMessages
{
  public class EditSubjectResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public long SubjectId { get; }

    public EditSubjectResponseMessage(ValidationResult validationResult, long subjectId)
    {
      ValidationResult = validationResult;
      SubjectId = subjectId;
    }

    public EditSubjectResponseMessage(ValidationResult validationResult)
    {
      ValidationResult = validationResult;
    }
  }
}