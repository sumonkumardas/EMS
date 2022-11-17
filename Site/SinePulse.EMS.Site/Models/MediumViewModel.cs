using SinePulse.EMS.Messages.Model.Enums;

namespace SinePulse.EMS.Site.Models
{
  public class MediumViewModel : BaseViewModel
  {
    public long MediumId { get; set; }
    public string MediumName { get; set; }
    public string MediumCode { get; set; }
    public StatusEnum Status { get; set; }
  }
}