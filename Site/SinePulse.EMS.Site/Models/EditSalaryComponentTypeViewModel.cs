using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Site.Models
{
  public class EditSalaryComponentTypeViewModel : BaseViewModel
  {
    public long SalaryComponentTypeId { get; set; }
    public string ComponentType { get; set; }
    public StatusEnum Status { get; set; }
  }
}
