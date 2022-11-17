using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Site.Models
{
  public class DesignationViewModel : BaseViewModel
  {
    public long Id { get; set; }
    public string DesignationName { get; set; }
    public EmployeeTypeEnum EmployeeType { get; set; }
    public StatusEnum Status { get; set; }
  }
}