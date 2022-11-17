using System.Collections.Generic;
using FluentValidation.Results;
using SinePulse.EMS.Messages.Model.Students;

namespace SinePulse.EMS.Messages.WaiverMessages
{
  public class GetStudentWaiversResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public IEnumerable<StudentWaiverMessageModel> StudentWaivers { get; }

    public GetStudentWaiversResponseMessage(ValidationResult validationResult,
      IEnumerable<StudentWaiverMessageModel> studentWaivers)
    {
      ValidationResult = validationResult;
      StudentWaivers = studentWaivers;
    }
  }
}