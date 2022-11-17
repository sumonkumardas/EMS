using MediatR;

namespace SinePulse.EMS.Messages.RoomMessages
{
  public class ShowRoomRequestMessage : IRequest<ShowRoomResponseMessage>
  {
    public long RoomId { get; set; }
  }
}