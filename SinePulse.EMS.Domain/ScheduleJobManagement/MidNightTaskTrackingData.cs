using System;
using SinePulse.EMS.Domain.Shared;

namespace SinePulse.EMS.Domain.ScheduleJobManagement
{
  public class MidNightTaskTrackingInfo : BaseEntity
  {
    public virtual DateTime Date { get; set; }
    public virtual DateTime Timestamp { get; set; }
  }
}