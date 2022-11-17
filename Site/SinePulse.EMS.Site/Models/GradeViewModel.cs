using SinePulse.EMS.Messages.Model.Enums;

namespace SinePulse.EMS.Site.Models
{
  public class GradeViewModel : BaseViewModel
  {
    public long Id { get; set; }
    public string GradeTitle { get; set; }
    public decimal MinSalary { get; set; }
    public decimal MaxSalary { get; set; }
    public StatusEnum Status { get; set; }
  }
}
