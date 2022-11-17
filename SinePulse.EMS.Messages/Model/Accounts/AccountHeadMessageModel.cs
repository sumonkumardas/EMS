using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Messages.Model.Shared;

namespace SinePulse.EMS.Messages.Model.Accounts
{
  public class AccountHeadMessageModel : BaseEntityMessageModel
  {
    #region Primitive Properties
    public string AccountCode { get; set; }
    public string AccountHeadName { get; set; }
    public AccountHeadTypeEnum AccountHeadType { get; set; }
    public AccountPeriodEnum AccountPeriod { get; set; }
    public StatusEnum Status { get; set; }
    public virtual bool IsLedger { get; set; }
    #endregion

    #region  Navigation Properties
    public AccountHeadMessageModel ParentHead { get; set; }
    public AccountTypeMessageModel AccountType { get; set; }
    #endregion
  }
}
