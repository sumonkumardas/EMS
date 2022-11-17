using MediatR;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Messages.LeaveTypeMessages
{
  public class ShowLeaveTypeRequestMessage : IRequest<ShowLeaveTypeResponseMessage>
  {
    public long LeaveTypeId { get; set; }
  }
}