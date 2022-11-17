using MediatR;

namespace SinePulse.EMS.Messages.BankMessages
{
  public class GetBankStatementInfoRequestMessage : IRequest<GetBankStatementInfoResponseMessage>
  {
    public string AccountNumber { get; set; }
    public long BranchMediumId { get; set; }
  }
}