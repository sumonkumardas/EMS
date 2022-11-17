using System;
using System.Collections.Generic;
using FluentValidation.Results;

namespace SinePulse.EMS.Messages.StudentSectionMessages
{
  public class ShowStudentSectionRollResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public int Roll { get;}

    public ShowStudentSectionRollResponseMessage(ValidationResult validationResult, int roll)
    {
      ValidationResult = validationResult;
      Roll = roll;
    }
  }
}