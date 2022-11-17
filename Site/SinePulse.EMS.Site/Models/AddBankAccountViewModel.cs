using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Site.Models
{
  public class AddBankAccountViewModel : BaseViewModel
  {
    public long BankBranchId { get; set; }
    public string AccountNumber { get; set; }
    public BankAccountTypeEnum AccountType { get; set; }
  }
}