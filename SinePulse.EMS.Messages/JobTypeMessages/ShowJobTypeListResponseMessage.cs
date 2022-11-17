using FluentValidation.Results;
using SinePulse.EMS.Messages.Model.Employees;
using System.Collections.Generic;

namespace SinePulse.EMS.Messages.JobTypeMessages
{
  public class ShowJobTypeListResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public List<JobTypeMessageModel> JobTypeList { get; }

    public ShowJobTypeListResponseMessage(ValidationResult validationResult, List<JobTypeMessageModel> jobTypeList)
    {
      ValidationResult = validationResult;
      JobTypeList = jobTypeList;
    }
  }
}