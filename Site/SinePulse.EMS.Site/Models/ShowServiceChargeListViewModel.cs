using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SinePulse.EMS.Site.Models
{
  public class ShowServiceChargeListViewModel : BaseViewModel
  {
    public List<ServiceChargeViewModel> serviceCharges { get; set; }
  }
}
