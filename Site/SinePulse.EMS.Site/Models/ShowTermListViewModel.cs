using System.Collections.Generic;
using SinePulse.EMS.Messages.Model.Banks;

namespace SinePulse.EMS.Site.Models
{
  public class ShowTermListViewModel : BaseViewModel
  {
    public List<TermViewModel> Terms { get; set; }
  }
}