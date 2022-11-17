using System;
using MediatR;

namespace SinePulse.EMS.Messages.TermMessages
{
  public class AddTermRequestMessage : IRequest<AddTermResponseMessage>
  {
    public string TermName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public long SessionId { get; set; }
    public long BranchMediumId { get; set; }
    public string CurrentUserName { get; set; }
  }
}