using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SinePulse.EMS.Site.Models
{
  public class PublicHolidayListViewModel: BaseViewModel
  {
    public List<PublicHolidayViewModel> Holidays = new List<PublicHolidayViewModel>();
  }
}
