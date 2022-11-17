using System.Collections.Generic;
using SinePulse.EMS.Messages.Model.Academic;

namespace SinePulse.EMS.Site.Models
{
  public class ShowShiftListViewModel : BaseViewModel
  {
    public IEnumerable<ShiftMessageModel> Shifts { get; set; }
  }
}