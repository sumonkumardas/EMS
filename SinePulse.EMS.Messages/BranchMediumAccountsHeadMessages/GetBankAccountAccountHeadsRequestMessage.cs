using MediatR;

namespace SinePulse.EMS.Messages.BranchMediumAccountsHeadMessages
{
  public class GetBankAccountAccountHeadsRequestMessage : IRequest<GetBankAccountAccountHeadsResponseMessage>
  {
    public long BranchMediumId { get; set; }
    public long SessionId { get; set; }
  }
}