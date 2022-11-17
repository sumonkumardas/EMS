using FluentValidation.Results;
using SinePulse.EMS.Messages.Model.Leaves;
using System.Collections.Generic;

namespace SinePulse.EMS.Messages.LeaveTypeMessages
{
  public class ShowLeaveTypeResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public LeaveTypeMessageModel LeaveType { get; set; }
    public ShowLeaveTypeResponseMessage(ValidationResult validationResult, LeaveTypeMessageModel leaveType)
    {
      ValidationResult = validationResult;
      LeaveType = leaveType;
    }
  }
}