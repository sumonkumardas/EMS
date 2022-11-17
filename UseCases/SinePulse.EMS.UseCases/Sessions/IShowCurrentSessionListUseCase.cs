using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Messages.SessionMessages;
using System;
using System.Collections.Generic;
using System.Text;

namespace SinePulse.EMS.UseCases.Sessions
{
  public interface IShowCurrentSessionListUseCase
  {
    IEnumerable<SessionMessageModel> ShowSessionList(long branchMediumId);
  }
}
