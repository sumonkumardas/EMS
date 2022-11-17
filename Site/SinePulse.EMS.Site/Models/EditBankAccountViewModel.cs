using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Site.Models
{
  public class EditBankAccountViewModel : BaseViewModel
  {
    public long BankAccountId { get; set; }
    public string AccountNumber { get; set; }
    public BankAccountTypeEnum AccountType { get; set; }
    public long BankBranchId { get; set; }
  }
}