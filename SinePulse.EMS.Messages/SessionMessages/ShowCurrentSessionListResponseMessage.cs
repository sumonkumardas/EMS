using System.Collections.Generic;
using FluentValidation.Results;
using SinePulse.EMS.Messages.Model.Academic;

namespace SinePulse.EMS.Messages.SessionMessages
{
  public class ShowCurrentSessionListResponseMessage
  {
    public IEnumerable<SessionMessageModel> Sessions { get; set; }

    public ShowCurrentSessionListResponseMessage(IEnumerable<SessionMessageModel> sessions)
    {
      Sessions = sessions;
    }
  }
}
