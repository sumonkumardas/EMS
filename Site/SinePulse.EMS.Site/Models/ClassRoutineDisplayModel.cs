using SinePulse.EMS.Messages.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SinePulse.EMS.Site.Models
{
  public class ClassRoutineDisplayModel : BaseViewModel
  {
    public DayOfWeek WeekDay { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public bool IsBreakTime { get; set; }
    public StatusEnum Status { get; set; }
  }
}
