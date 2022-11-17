using SinePulse.EMS.Messages.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SinePulse.EMS.Site.Models
{
  public class BranchMediumAccountsHeadViewModel : BaseViewModel
  {
    #region Primitive Properties
    public long Id { get; set; }
    public virtual string AccountCode { get; set; }
    public virtual string AccountHeadName { get; set; }
    public virtual AccountHeadTypeEnum AccountHeadType { get; set; }
    public virtual AccountPeriodEnum AccountPeriod { get; set; }
    public virtual StatusEnum Status { get; set; }
    public virtual bool IsLedger { get; set; }
    #endregion

    #region  Navigation Properties
    public virtual AccountTypeViewModel AccountType { get; set; }
    public virtual AccountHeadViewModel ParentHead { get; set; }
    public virtual BranchMediumViewModel BranchMedium { get; set; }
    public virtual SessionViewModel Session { get; set; }
    #endregion
  }
}
