
using SinePulse.EMS.Messages.Model.Enums;
using System;

namespace SinePulse.EMS.Messages.EmployeeLeaveMessages
{
  public class ApproveLeaveRequestMessage : MediatR.IRequest<ApproveLeaveResponseMessage>
  {
    public LeaveStatusEnum LeaveStatus { get; set; }
    public long LeaveId { get; set; }
    public string CurrentUserName { get; set; }
  }
}