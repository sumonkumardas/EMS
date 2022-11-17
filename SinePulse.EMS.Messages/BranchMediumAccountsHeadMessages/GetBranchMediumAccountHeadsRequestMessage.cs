using MediatR;
using SinePulse.EMS.Domain.Enums;


namespace SinePulse.EMS.Messages.BranchMediumAccountsHeadMessages
{
  public class GetBranchMediumAccountHeadsRequestMessage : IRequest<GetBranchMediumAccountHeadsResponseMessage>
  {
    public AccountPeriodEnum AccountPeriod { get; set; }
    public long BranchMediumId { get; set; }
  }
}