using SinePulse.EMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SinePulse.EMS.Site.Models
{
  public class SalaryComponentTypeViewModel : BaseViewModel
  {
    public long Id { get; set; }
    public string ComponentType { get; set; }
    public StatusEnum Status { get; set; }
  }
}
