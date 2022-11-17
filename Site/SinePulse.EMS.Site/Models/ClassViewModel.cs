using System.Collections.Generic;
using SinePulse.EMS.Messages.ClassMessages;
using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Messages.Model.Enums;

namespace SinePulse.EMS.Site.Models
{
  public class ClassViewModel : BaseViewModel
  {
    public long Id { get; set; }
    public string ClassName { get; set; }
    public int ClassCode { get; set; }
    public StatusEnum Status { get; set; }
    public IEnumerable<GetAddedSubjectsOfClassResponseMessage.SubjectWithClass> Subjects { get; set; }
  }
}