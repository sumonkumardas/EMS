using MediatR;

namespace SinePulse.EMS.Messages.BankBranchMessages
{
  public class ShowBankBranchRequestMessage : IRequest<ShowBankBranchResponseMessage>
  {
    public long BankBranchId { get; set; }
  }
}