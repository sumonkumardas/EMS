using FluentValidation.Results;
using SinePulse.EMS.Domain.Academic;
using System.Collections.Generic;

namespace SinePulse.EMS.Messages.InstituteMessages
{
  public class ShowInstituteResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public Institute Institute { get; }

    public ShowInstituteResponseMessage(ValidationResult validationResult, Institute institute)
    {
      ValidationResult = validationResult;
      Institute = institute;
    }
  }
}