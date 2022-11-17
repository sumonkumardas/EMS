using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Site.Models
{
  public class AddRoomViewModel : BaseViewModel
  {
    public string RoomNo { get; set; }
    public int ClassTimeCapacity { get; set; }
    public int ExamTimeCapacity { get; set; }
    public ObjectTypeEnum ObjectType { get; set; }
    public new long BranchId { get; set; }
    public long ObjectId { get; set; }
  }
}