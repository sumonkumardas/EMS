using System;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Site.Models
{
  public class AddShiftViewModel : BaseViewModel
  {
    public long BranchId { get; set; }
    public long BranchMediumId { get; set; }
    public string ShiftName { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public ObjectTypeEnum ShiftType { get; set; }
  }
}