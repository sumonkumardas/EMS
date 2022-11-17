using System.Collections.Generic;
using SinePulse.EMS.Messages.Model.Enums;

namespace SinePulse.EMS.Site.Models
{
  public class ShowSeatPlanListViewModel : BaseViewModel
  {
    public List<SeatPlanViewModel> SeatPlans { get; set; }
  }
}