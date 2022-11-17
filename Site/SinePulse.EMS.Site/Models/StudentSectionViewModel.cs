using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SinePulse.EMS.Site.Models
{
  public class StudentSectionViewModel : BaseViewModel
  {
    public int RollNo { get; set; }
    public StudentViewModel Student { get; set; }
    public SectionViewModel Section { get; set; }
  }
}
