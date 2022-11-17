using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SinePulse.EMS.Site.Models
{
  public class TreeViewModel : BaseViewModel
  {
    public long Id { get; set; }
    public int Level { get; set; }
    public string Name { get; set; }
    public long ParentId { get; set; }
  }
}
