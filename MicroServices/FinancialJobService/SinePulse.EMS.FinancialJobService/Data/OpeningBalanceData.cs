using System.Collections.Generic;

namespace SinePulse.EMS.FinancialJobService.Data
{
  public class OpeningBalanceData
  {
    public ICollection<AccountCodeBalance> AssetAccountCodeBalances { get; set; }
    public ICollection<AccountCodeBalance> LiabilityAccountCodeBalances { get; set; }
    public ICollection<AccountCodeBalance> EquityAccountCodeBalances { get; set; }

    public class AccountCodeBalance
    {
      public string AccountCode { get; set; }
      public decimal Balance { get; set; }
    }
  }
}