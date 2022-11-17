using SinePulse.EMS.Messages.Model.Enums;

namespace SinePulse.EMS.Site.Models
{
  public class ExamRoutineViewModel : BaseViewModel
  {
    public string ExamRoutineName { get; set; }
    public TermViewModel Term { get; set; }
    public StatusEnum Status { get; set; }
  }
}
