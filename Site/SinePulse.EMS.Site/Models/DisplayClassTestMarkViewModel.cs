using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SinePulse.EMS.Messages.Model.Academic;

namespace SinePulse.EMS.Site.Models
{
  public class DisplayClassTestMarkViewModel : BaseViewModel
  {
    public List<SessionMessageModel> Sessions { get; set; }
    public long SessionId { get; set; }
    public long TermId { get; set; }
    public string ClassName { get; set; }
    public int Roll  { get; set; }
    public long StudentId { get; set; }
  }
}
