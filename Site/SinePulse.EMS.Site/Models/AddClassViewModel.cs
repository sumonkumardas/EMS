using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Site.Models
{
  public class AddClassViewModel: BaseViewModel
  {
    public string ClassName { get; set; }
    public int ClassCode { get; set; }
    public StatusEnum Status { get; set; }
  }
}