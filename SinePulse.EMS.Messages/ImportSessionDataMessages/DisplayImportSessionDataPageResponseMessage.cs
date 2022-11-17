using System;
using System.Collections.Generic;

namespace SinePulse.EMS.Messages.ImportSessionDataMessages
{
  public class DisplayImportSessionDataPageResponseMessage
  {
    public ICollection<Session> PreviousSessions { get; }
    public Session CurrentSession { get; }

    public DisplayImportSessionDataPageResponseMessage(ICollection<Session> previousSessions, Session currentSession)
    {
      PreviousSessions = previousSessions;
      CurrentSession = currentSession;
    }

    public class Session
    {
      public long SessionId { get; set; }
      public string SessionName { get; set; }
      public DateTime StartDate { get; set; }
      public DateTime EndDate { get; set; }
    }
  }
}