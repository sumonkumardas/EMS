using SinePulse.EMS.Domain.Accounts;
using SinePulse.EMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SinePulse.EMS.Site.Models
{
  public class TransactionTypeAndAccountTypeViewModel : BaseViewModel
  {
    public TransactionTypeAndAccountTypeViewModel()
    {
      TransactionTypeList = ((AccountTransactionTypeEnum[])Enum.GetValues(typeof(AccountTransactionTypeEnum))).Select(c => new EnumViewModel() { Value = (int)c, Name = c.ToString() }).ToList();

      AccountTypeList = ((ChartOfAccountTypeEnum[])Enum.GetValues(typeof(ChartOfAccountTypeEnum))).Select(c => new EnumViewModel() { Value = (int)c, Name = c.ToString() }).ToList();
    }
    public List<EnumViewModel> TransactionTypeList { get; set; }
    public List<EnumViewModel> AccountTypeList { get; set; }
  }
}
