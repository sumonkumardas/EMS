using System.Collections.Generic;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Messages.Model.Accounts;

namespace SinePulse.EMS.Site.Models
{
  public class AddAccountHeadViewModel: BaseViewModel
  {
    public string AccountCode { get; set; }
    public string AccountHeadName { get; set; }
    public AccountHeadTypeEnum AccountHeadType { get; set; }
    public AccountPeriodEnum AccountPeriod { get; set; }
    public string ParentHeadId { get; set; }
    public IEnumerable<AccountHeadMessageModel> AccountHeads { get; set; }
  }
}