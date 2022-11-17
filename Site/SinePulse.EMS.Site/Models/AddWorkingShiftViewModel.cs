using System;

namespace SinePulse.EMS.Site.Models
{
  public class AddWorkingShiftViewModel : BaseViewModel
  {
    public string ShiftName { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
  }
}