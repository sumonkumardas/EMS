using System;
using System.Collections.Generic;
using SinePulse.EMS.ProjectPrimitives;

namespace SinePulse.EMS.Site.Models
{
  public class AddBreakTimeViewModel: BaseViewModel
  {
    public new long BranchMediumId { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
  }
}