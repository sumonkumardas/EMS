using FluentValidation.Results;
using SinePulse.EMS.Messages.Model.Leaves;
using System.Collections.Generic;

namespace SinePulse.EMS.Messages.EmployeeLeaveMessages
{
  public class ShowEmployeeLeaveListResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public List<EmployeeLeaveMessageModel> UnapprovedLeaveList { get; set; }
    public ShowEmployeeLeaveListResponseMessage(ValidationResult validationResult, List<EmployeeLeaveMessageModel> unapprovedLeaveList)
    {
      ValidationResult = validationResult;
      UnapprovedLeaveList = unapprovedLeaveList;
    }
  }
}