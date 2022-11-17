using MediatR;

namespace SinePulse.EMS.Messages.EmployeeLeaveMessages
{
  public class GetPendingLeavesRequestMessage : IRequest<GetPendingLeavesResponseMessage>
  {
    public long EmployeeId { get; set; }
  }
}
