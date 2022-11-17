using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Site.Models
{
  public class AddMediumViewModel : BaseViewModel
  {
    public string MediumName { get; set; }
    public string MediumCode { get; set; }
    public StatusEnum Status { get; set; }
  }
}