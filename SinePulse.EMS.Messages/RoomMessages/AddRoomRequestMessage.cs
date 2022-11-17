using MediatR;

namespace SinePulse.EMS.Messages.RoomMessages
{
  public class AddRoomRequestMessage : IRequest<AddRoomResponseMessage>
  {
    public string RoomNo { get; set; }
    public int ClassTimeCapacity { get; set; }
    public int ExamTimeCapacity { get; set; }
    public long BranchId { get; set; }
    public string CurrentUserName { get; set; }
  }
}