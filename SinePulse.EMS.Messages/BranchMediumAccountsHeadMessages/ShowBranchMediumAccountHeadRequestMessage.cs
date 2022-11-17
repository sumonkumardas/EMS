using MediatR;
using SinePulse.EMS.Domain.Enums;


namespace SinePulse.EMS.Messages.BranchMediumAccountsHeadMessages
{
  public class ShowBranchMediumAccountHeadRequestMessage : IRequest<ShowBranchMediumAccountHeadResponseMessage>
  {
    public long CurrentSessionId { get; set; }
    public long BranchMediumId { get; set; }
    public long AccountHeadId { get; set; }
  }
}