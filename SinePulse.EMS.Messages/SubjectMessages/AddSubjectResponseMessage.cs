using FluentValidation.Results;

namespace SinePulse.EMS.Messages.SubjectMessages
{
  public class AddSubjectResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public long SubjectId { get;}

    public AddSubjectResponseMessage(ValidationResult validationResult)
    {
      ValidationResult = validationResult;
    }
    
    public AddSubjectResponseMessage(ValidationResult validationResult, long subjectId)
    {
      ValidationResult = validationResult;
      SubjectId = subjectId;
    }
  }
}