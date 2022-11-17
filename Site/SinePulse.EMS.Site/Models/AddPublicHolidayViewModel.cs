using SinePulse.EMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SinePulse.EMS.Site.Models
{
  public class AddPublicHolidayViewModel : BaseViewModel
  {
    public string HolidayName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public StatusEnum Status { get; set; }
  }
}
