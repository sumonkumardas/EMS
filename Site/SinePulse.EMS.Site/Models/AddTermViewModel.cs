using System;
using System.Collections.Generic;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Site.Models
{
  public class AddTermViewModel : BaseViewModel
  {
    public long BranchMediumId { get; set; }
    public string TermName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public StatusEnum Status { get; set; }
    public long SessionId { get; set; }
    public ICollection<Session> Sessions { get; set; }

    public class Session
    {
      public long SessionId { get; set; }
      public string SessionText { get; set; }
    }
  }
}