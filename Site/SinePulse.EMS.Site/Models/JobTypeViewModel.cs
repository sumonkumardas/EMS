using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Site.Models
{
  public class JobTypeViewModel : BaseViewModel
  {
    public long Id { get; set; }
    public string JobTitle { get; set; }
    public bool HasOverTime { get; set; }
    public StatusEnum Status { get; set; }
  }
}