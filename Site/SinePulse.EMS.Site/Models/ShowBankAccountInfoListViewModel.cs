using System.Collections.Generic;
using SinePulse.EMS.Messages.Model.Banks;

namespace SinePulse.EMS.Site.Models
{
  public class ShowBankAccountInfoListViewModel : BaseViewModel
  {
    public List<BankAccountInfoViewModel> BankAccountInfos { get; set; }
  }
}