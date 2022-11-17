using System.Collections.Generic;
using SinePulse.EMS.Messages.Model.Academic;

namespace SinePulse.EMS.Site.Models
{
  public class ShowSessionListViewModel : BaseViewModel
  {
    public IEnumerable<SessionMessageModel> Sessions { get; set; }
  }
}