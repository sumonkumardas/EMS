using System;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Site.Models
{
  public class AddSessionViewModel : BaseViewModel
  {
    public string SessionName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsSessionClosed { get; set; }
    public ObjectTypeEnum SessionType { get; set; }
    public long ObjectId { get; set; }
  }
}