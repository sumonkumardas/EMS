using MediatR;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Messages.BranchMediumAccountsHeadMessages
{
  public class ShowBranchMediumAccountsHeadListRequestMessage : IRequest<ShowBranchMediumAccountsHeadListResponseMessage>
  {
    public long BranchMediumId { get; set; }
    public string ParentAccountHeadCode { get; set; }
    public long CurrentSessionId { get; set; }
  }
}