using System;
using FluentValidation.Results;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Messages.BreakTimeMessages
{
  public class GetClassBreakTimeResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public BreakTimeProperty BreakTimeProperties { get; }

    public GetClassBreakTimeResponseMessage(ValidationResult validationResult, BreakTimeProperty breakTimeProperties)
    {
      ValidationResult = validationResult;
      BreakTimeProperties = breakTimeProperties;
    }

    public class BreakTimeProperty
    {
      public TimeSpan StartTime { get; set; }
      public TimeSpan EndTime { get; set; }
      public StatusEnum Status { get; set; }
    }
  }
}