using System;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Site.Models
{
  public class ShiftViewModel : BaseViewModel
  {
    public long BranchId { get; set; }
    public long ShiftId { get; set; }
    public string ShiftName { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public StatusEnum Status { get; set; }
  }
}