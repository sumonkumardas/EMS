using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Shared;

namespace SinePulse.EMS.Domain.Accounts
{
  public class AccountHead : BaseEntity
  {
    public override string Title()
    {
      string title = this.AccountHeadName + " (" + this.AccountCode + ")";
      title = title + " - " + AccountHeadType.ToString();

      if (this.AccountHeadType == AccountHeadTypeEnum.Academic || this.AccountHeadType == AccountHeadTypeEnum.Payroll)
      {
        title = title + " - " + AccountPeriod.ToString();
      }

      return title;
    }

    #region Primitive Properties
    public virtual string AccountCode { get; set; }
    public virtual string AccountHeadName { get; set; }
    public virtual AccountHeadTypeEnum AccountHeadType { get; set; }
    public virtual AccountPeriodEnum AccountPeriod { get; set; }
    public virtual StatusEnum Status { get; set; }
    public virtual bool IsLedger { get; set; }
    #endregion

    #region Complex Properties
    private AuditFields _auditFields = new AuditFields();

    public virtual AuditFields AuditFields
    {
      get { return _auditFields; }
      set { _auditFields = value; }
    }
    #endregion

    #region  Navigation Properties
    public virtual AccountType AccountType { get; set; }
    public virtual AccountHead ParentHead { get; set; }
    #endregion

      }
}
