using System;
using MediatR;

namespace SinePulse.EMS.Messages.EmployeeLeaveMessages
{
  public class GetEmployeeLeavesRequestMessage : IRequest<GetEmployeeLeavesResponseMessage>
  {
    public long EmployeeId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
  }
}