using FluentValidation.Results;

namespace SinePulse.EMS.Messages.BreakTimeMessages
{
  public class AddBreakTimeResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public long BreakTimeId { get; }
    public AddBreakTimeResponseMessage(ValidationResult validationResult, long breakTimeId)
    {
      ValidationResult = validationResult;
      BreakTimeId = breakTimeId;
    } 
  }
}