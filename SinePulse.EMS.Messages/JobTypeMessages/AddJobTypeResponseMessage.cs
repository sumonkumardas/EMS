using FluentValidation.Results;

namespace SinePulse.EMS.Messages.JobTypeMessages
{
  public class AddJobTypeResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public long JobTypeId { get; }

    public AddJobTypeResponseMessage(ValidationResult validationResult, long jobTypeId)
    {
      ValidationResult = validationResult;
      JobTypeId = jobTypeId;
    }
    
    public AddJobTypeResponseMessage(ValidationResult validationResult)
    {
      ValidationResult = validationResult;
    }
  }
}