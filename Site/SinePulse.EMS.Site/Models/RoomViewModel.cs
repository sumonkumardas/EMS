using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Messages.Model.Academic;

namespace SinePulse.EMS.Site.Models
{
  public class RoomViewModel : BaseViewModel
  {
    public long Id { get; set; }
    public string RoomNo { get; set; }
    public int ClassTimeCapacity { get; set; }
    public int ExamTimeCapacity { get; set; }
    public StatusEnum Status { get; set; }
    public BranchMessageModel Branch { set; get; }
  }
}