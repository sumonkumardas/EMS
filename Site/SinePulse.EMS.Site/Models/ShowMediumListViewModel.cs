using System.Collections.Generic;
using SinePulse.EMS.Messages.Model.Academic;

namespace SinePulse.EMS.Site.Models
{
  public class ShowMediumListViewModel : BaseViewModel
  {
    public IEnumerable<MediumMessageModel> Mediums { get; set; }
  }
}