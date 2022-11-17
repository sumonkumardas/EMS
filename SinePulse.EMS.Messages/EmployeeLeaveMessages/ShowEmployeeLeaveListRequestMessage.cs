using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.ProjectPrimitives;
using System;

namespace SinePulse.EMS.Messages.EmployeeLeaveMessages
{
  public class ShowEmployeeLeaveListRequestMessage : MediatR.IRequest<ShowEmployeeLeaveListResponseMessage>
  {
    public LeaveStatusEnum LeaveStatus { get; set; }
    public long? EmployeeId { get; set; }
    public long? BranchMediumId { get; set; }
        public int Year { get; set; }
  }
}