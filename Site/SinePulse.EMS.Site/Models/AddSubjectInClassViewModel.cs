using System.Collections.Generic;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Messages.Model.Academic;

namespace SinePulse.EMS.Site.Models
{
  public class AddSubjectInClassViewModel : BaseViewModel
  {
    public long ClassId { get; set; }
    public string ClassName { get; set; }
    public int ClassCode { get; set; }
    public GroupEnum Group { get; set; }
    public int[] SubjectIds { get; set; }
    public bool IsOptional { get; set; }
    public IEnumerable<SubjectMessageModel> Subjects { get; set; }
  }
}