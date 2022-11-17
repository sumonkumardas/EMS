using MediatR;

namespace SinePulse.EMS.Messages.BankBranchMessages
{
  public class AddBankBranchRequestMessage : IRequest<AddBankBranchResponseMessage>
  {
    public string BranchName { get; set; }
    public string Address { get; set; }
    public long BankId { get; set; }
    public string CurrentUserName { get; set; }
  }
}