using MediatR;

namespace SinePulse.EMS.Messages.BankBranchMessages
{
  public class EditBankBranchRequestMessage : IRequest<EditBankBranchResponseMessage>
  {
    public long BankBranchId { get; set; }
    public string BranchName { get; set; }
    public string Address { get; set; }
    public string CurrentUserName { get; set; }
  }
}