using MediatR;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Messages.RoomMessages
{
  public class EditRoomRequestMessage : IRequest<EditRoomResponseMessage>
  {
    public long Id { get; set; }
    public string RoomNo { get; set; }
    public int ClassTimeCapacity { get; set; }
    public int ExamTimeCapacity { get; set; }
    public StatusEnum Status { get; set; }
    public string CurrentUserName { get; set; }
    public long BranchId { get; set; }
  }
}