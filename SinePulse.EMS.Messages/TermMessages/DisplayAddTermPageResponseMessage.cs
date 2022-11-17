using System.Collections.Generic;
using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Messages.SessionMessages;

namespace SinePulse.EMS.Messages.TermMessages
{
  public class DisplayAddTermPageResponseMessage
  {
    public ICollection<SessionMessageModel> Sessions { get; }

    public DisplayAddTermPageResponseMessage(ICollection<SessionMessageModel> sessions)
    {
      Sessions = sessions;
    }
  }
}