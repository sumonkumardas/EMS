using FluentValidation.Results;

namespace SinePulse.EMS.Messages.EmployeeLeaveMessages
{
  public class ApproveLeaveResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public long ApproverId { get; set; }
    public ApproveLeaveResponseMessage(ValidationResult validationResult,long approverId)
    {
      ValidationResult = validationResult;
      ApproverId = approverId;
    }

    
  }
}