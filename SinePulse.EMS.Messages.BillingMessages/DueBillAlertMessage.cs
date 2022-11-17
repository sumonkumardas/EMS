using System;
using System.Collections.Generic;
using System.Text;

namespace SinePulse.EMS.Messages.BillingMessages
{
  public class DueBillAlertMessage
  {
    public long BranchMediumId { get; set; }
    public DateTime ScheduleTimestamp { get; set; }
  }
}
