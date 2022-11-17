using System.Collections.Generic;
using MediatR;
using SinePulse.EMS.Messages.Model.Academic;

namespace SinePulse.EMS.Messages.BranchMediumMessages
{
  public class ShowBranchMediumRequestMessage : IRequest<ShowBranchMediumResponseMessage>
  {
    public long BranchMediumId { get; set; }
    public ICollection<SessionMessageModel> InstituteSessions { get; set; }
    public InstituteMessageModel Institute { get; set; }
    public SessionMessageModel CurrentSession { get; set; }
  }
}