using MediatR;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Messages.BankAccountMessages
{
  public class AddBankAccountRequestMessage : IRequest<AddBankAccountResponseMessage>
  {
    public long BankBranchId { get; set; }
    public string AccountNumber { get; set; }
    public BankAccountTypeEnum AccountType { get; set; }
    public string CurrentUserName { get; set; }
  }
}