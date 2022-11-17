using System.Collections.Generic;
using MediatR;
using SinePulse.EMS.Messages.Model.Accounts;

namespace SinePulse.EMS.Messages.BranchMediumMessages
{
  public class DisplayBranchMediumRequestMessage : IRequest<DisplayBranchMediumResponseMessage>
  {
    public long BranchMediumId { get; set; }
    public bool LoadSessions { get; set; } 
  }
}