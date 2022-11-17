using System.Collections.Generic;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Messages.Model.Accounts;

namespace SinePulse.EMS.Site.Models
{
  public class AccountHeadViewModel: BaseViewModel
  {
    public long Id { get; set; }
    public string AccountCode { get; set; }
    public string AccountHeadName { get; set; }
    public AccountHeadTypeEnum AccountHeadType { get; set; }
    public AccountPeriodEnum AccountPeriod { get; set; }
    public AccountHeadMessageModel ParentHead { get; set; }
    public StatusEnum Status { get; set; }
    public long AccountHeadId { get; set; }
    public long ParentHeadId { get; set; }
    public IEnumerable<AccountHeadMessageModel> AccountHeads { get; set; }
  }
}