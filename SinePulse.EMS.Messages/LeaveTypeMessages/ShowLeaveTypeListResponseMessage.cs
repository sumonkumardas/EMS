using FluentValidation.Results;
using SinePulse.EMS.Messages.Model.Leaves;
using System.Collections.Generic;

namespace SinePulse.EMS.Messages.LeaveTypeMessages
{
  public class ShowLeaveTypeListResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public List<LeaveTypeMessageModel> LeaveTypeList { get; set; }
    public ShowLeaveTypeListResponseMessage(ValidationResult validationResult, List<LeaveTypeMessageModel> leaveTypeList)
    {
      ValidationResult = validationResult;
      LeaveTypeList = leaveTypeList;
    }
  }
}