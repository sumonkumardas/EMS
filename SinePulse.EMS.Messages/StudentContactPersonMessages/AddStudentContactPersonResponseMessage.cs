using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace SinePulse.EMS.Messages.StudentContactPersonMessages
{
    public class AddStudentContactPersonResponseMessage
    {
    public ValidationResult ValidationResult { get; }
    public long ContactPersonId { get; }
    public AddStudentContactPersonResponseMessage(ValidationResult validationResult, long contactPersonId)
    {
      ValidationResult = validationResult;
      ContactPersonId = contactPersonId;
    }
  }
}
