using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Site.Models
{
  public class AddEmployeeGradeViewModel : BaseViewModel
  {
    public string GradeTitle { get; set; }
    public decimal MinSalary { get; set; }
    public decimal MaxSalary { get; set; }
    public StatusEnum Status { get; set; }
  }
}