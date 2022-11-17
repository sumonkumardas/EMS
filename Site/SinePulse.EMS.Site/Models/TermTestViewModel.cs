using System;

namespace SinePulse.EMS.Site.Models
{
  public class TermTestViewModel: BaseViewModel
  {
    public long TermTestId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
  }
}