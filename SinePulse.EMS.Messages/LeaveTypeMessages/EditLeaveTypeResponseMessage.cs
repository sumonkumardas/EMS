using FluentValidation.Results;

namespace SinePulse.EMS.Messages.LeaveTypeMessages
{
  public class EditLeaveTypeResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public EditLeaveTypeResponseMessage(ValidationResult validationResult)
    {
      ValidationResult = validationResult;
    }
  }
}