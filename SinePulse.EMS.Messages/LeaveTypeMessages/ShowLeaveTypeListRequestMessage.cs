using MediatR;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Messages.LeaveTypeMessages
{
  public class ShowLeaveTypeListRequestMessage : IRequest<ShowLeaveTypeListResponseMessage>
  {
  }
}