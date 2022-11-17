using MediatR;

namespace SinePulse.EMS.Messages.SectionMessages
{
  public class AssignOrChangeRoomInSectionRequestMessage : IRequest<AssignOrChangeRoomInSectionResponseMessage>
  {
    public long SectionId { get; set; }
    public long RoomId { get; set; }
  }
}