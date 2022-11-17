using System;
using System.Collections.Generic;
using FluentValidation.Results;

namespace SinePulse.EMS.Messages.EmployeeLeaveMessages
{
  public class GetPendingLeavesResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public IEnumerable<PendingLeave> PendingLeaves { get; }

    public GetPendingLeavesResponseMessage(ValidationResult validationResult, IEnumerable<PendingLeave> pendingLeaves)
    {
      ValidationResult = validationResult;
      PendingLeaves = pendingLeaves;
    }

    public class PendingLeave
    {
      public long PendingLeaveId { get; set; }
      public long LeaveTypeId { get; set; }
      public string LeaveTypeName { get; set; }
      public string Reason { get; set; }
      public DateTime StartDate { get; set; }
      public DateTime EndDate { get; set; }
      public string LeaveStatus { get; set; }
      public long EmployeeId { get; set; }
      public string EmployeeName { get; set; }
    }
  }
}