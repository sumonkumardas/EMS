using MediatR;

namespace SinePulse.EMS.Messages.RoomMessages
{
  public class ShowRoomsListRequestMessage : IRequest<ShowRoomsListResponseMessage>
  {
    public long SectionId { get; set; }
  }
}