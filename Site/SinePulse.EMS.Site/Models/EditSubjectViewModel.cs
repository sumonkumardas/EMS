using System.Collections.Generic;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Messages.Model.Academic;

namespace SinePulse.EMS.Site.Models
{
  public class EditSubjectViewModel : BaseViewModel
  {
    public long SubjectId { get; set; }
    public string SubjectName { get; set; }
    public int SubjectCode { get; set; }
    public StatusEnum Status { get; set; }
  }
}