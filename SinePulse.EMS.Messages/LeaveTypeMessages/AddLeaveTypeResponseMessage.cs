using FluentValidation.Results;

namespace SinePulse.EMS.Messages.LeaveTypeMessages
{
  public class AddLeaveTypeResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public long LeaveTypeId { get; set; }
    public AddLeaveTypeResponseMessage(ValidationResult validationResult,long leaveTypeId)
    {
      ValidationResult = validationResult;
      LeaveTypeId = leaveTypeId;
    }
  }
}