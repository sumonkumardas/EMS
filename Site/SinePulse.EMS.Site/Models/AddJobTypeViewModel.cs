using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Site.Models
{
  public class AddJobTypeViewModel : BaseViewModel
  {
    public string Title { get; set; }
    public bool HasOverTime { get; set; }
    public StatusEnum Status { get; set; }
  }
}