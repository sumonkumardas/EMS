using SinePulse.EMS.Messages.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SinePulse.EMS.Site.Models
{
  public class TestViewModel : BaseViewModel
  {
    public long Id { get; set; }
    public string TestName { get; set; }
    public string TestDescription { get; set; }
    public decimal Weight { get; set; }
    public decimal FullMarks { get; set; }
    public DateTime StartTimestamp { get; set; }
    public DateTime EndTimestamp { get; set; }
    public StatusEnum Status { get; set; }
  }
}
