using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Messages.Model.Leaves;
using SinePulse.EMS.ProjectPrimitives;
using System;
using System.Collections.Generic;

namespace SinePulse.EMS.Messages.EmployeeLeaveMessages
{
  public class AddEmployeeLeaveRequestMessage : MediatR.IRequest<AddEmployeeLeaveResponseMessage>
  {
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Reason { get; set; }
    public LeaveStatusEnum LeaveStatus { get; set; }
    public long EmployeeId { get; set; }
    public long LeaveTypeId { get; set; }
    public string CurrentUserName { get; set; }
  }
}