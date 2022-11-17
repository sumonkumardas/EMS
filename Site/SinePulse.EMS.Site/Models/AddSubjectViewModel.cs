using System.Collections.Generic;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Messages.Model.Academic;

namespace SinePulse.EMS.Site.Models
{
  public class AddSubjectViewModel : BaseViewModel
  {
    public string SubjectName { get; set; }
    public int SubjectCode { get; set; }
  }
}