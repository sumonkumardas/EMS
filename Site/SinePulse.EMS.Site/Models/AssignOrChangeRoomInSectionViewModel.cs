using System.Collections.Generic;

namespace SinePulse.EMS.Site.Models
{
  public class AssignOrChangeRoomInSectionViewModel : BaseViewModel
  {
    public List<RoomViewModel> Rooms { get; set; }
    public long SectionId { get; set; }
    public long RoomId { get; set; }
  }
}