using MediatR;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Messages.BankAccountMessages
{
  public class EditBankAccountRequestMessage : IRequest<EditBankAccountResponseMessage>
  {
    public long BankAccountId { get; set; }
    public string AccountNumber { get; set; }
    public BankAccountTypeEnum AccountType { get; set; }
    public string CurrentUserName { get; set; }
    public long BankBranchId { get; set; }
  }
}