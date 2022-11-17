using MediatR;

namespace SinePulse.EMS.Messages.BankMessages
{
  public class ShowBankListRequestMessage : IRequest<ShowBankListResponseMessage>
  {
    public long BranchMediumId { get; set; }
  }
}