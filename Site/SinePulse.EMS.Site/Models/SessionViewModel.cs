using SinePulse.EMS.Domain.Enums;
using System;
using SinePulse.EMS.Messages.Model.Academic;

namespace SinePulse.EMS.Site.Models
{
  public class SessionViewModel : BaseViewModel
  {
    public string SessionName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsSessionClosed { get; set; }
    public StatusEnum Status { get; set; }
    public ObjectTypeEnum SessionType { get; set; }
    public long ObjectId { get; set; }
    public long Id { get; set; }
    public InstituteMessageModel Institute { get; set; }
  }
}