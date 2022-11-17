using FluentValidation.Results;
using SinePulse.EMS.Messages.Model.Employees;

namespace SinePulse.EMS.Messages.JobTypeMessages
{
  public class ShowJobTypeResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public JobTypeMessageModel JobType { get; }

    public ShowJobTypeResponseMessage(ValidationResult validationResult, JobTypeMessageModel jobType)
    {
      ValidationResult = validationResult;
      JobType = jobType;
    }
  }
}