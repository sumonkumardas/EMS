using NakedObjects;
using SinePulse.EMS.NO.Domain.Enums;
using SinePulse.EMS.NO.Domain.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinePulse.EMS.NO.Domain.Accounts
{
  [Table("AccountHeads")]
  [DisplayName("Account Head")]
  [Bounded]
  public class NOAccountHead : NOBaseEntity
  {
    #region Injected Services
    public IDomainObjectContainer Container { set; protected get; }
    #endregion

    #region Life Cycle Methods
    public virtual void Persisting()
    {
      AuditFields.InsertedBy = Container.Principal.Identity.Name;
      AuditFields.InsertedDateTime = DateTime.Now;
      AuditFields.LastUpdatedBy = Container.Principal.Identity.Name;
      AuditFields.LastUpdatedDateTime = DateTime.Now;
    }
    public virtual void Updating()
    {
      AuditFields.LastUpdatedBy = Container.Principal.Identity.Name;
      AuditFields.LastUpdatedDateTime = DateTime.Now;
    }
    #endregion

    public string Title()
    {
      var t = Container.NewTitleBuilder();

      string title = this.AccountHeadName + " (" + this.AccountCode + ")";
      title = title + " - " + AccountHeadType.ToString();

      if (this.AccountHeadType == AccountHeadTypeEnum.Academic || this.AccountHeadType == AccountHeadTypeEnum.Payroll)
      {
        title = title + " - " + AccountPeriod.ToString();
      }
      t.Append(title);

      return t.ToString();
    }

    #region Primitive Properties
    [Required, StringLength(50), MemberOrder(10)]
    public virtual string AccountCode { get; set; }
    [Required, StringLength(50), MemberOrder(20)]
    public virtual string AccountHeadName { get; set; }
    [Required, MemberOrder(40)]
    public virtual AccountHeadTypeEnum AccountHeadType { get; set; }
    [Required, MemberOrder(50)]
    public virtual AccountPeriodEnum AccountPeriod { get; set; }
    [Required, MemberOrder(70)]
    public virtual bool IsLedger { get; set; }
    [Required, MemberOrder(80)]
    public virtual StatusEnum Status { get; set; }
    #endregion

    #region Complex Properties
    private AuditFields _auditFields = new AuditFields();

    public virtual AuditFields AuditFields
    {
      get { return _auditFields; }
      set { _auditFields = value; }
    }
    public bool HideAuditFields()
    {
      return true;
    }
    #endregion

    #region  Navigation Properties
    [Required, MemberOrder(30)]
    public virtual NOAccountType AccountType { get; set; }
    [Optionally, MemberOrder(60)]
    public virtual NOAccountHead ParentHead { get; set; }
    #endregion

    #region Get Properties
    [NotMapped, MemberOrder(55)]
    [Eagerly(EagerlyAttribute.Do.Rendering)]
    [TableView(true, "AccountCode", "AccountHeadName", "AccountHeadType", "AccountPeriod")]
    public IList<NOAccountHead> Childs
    {
      get
      {
        return Container.Instances<NOAccountHead>().Where(w => w.ParentHead.Id == this.Id).OrderBy(o => o.AccountCode).ToList();
      }
    }
    #endregion

    #region Behavior
    public void AddChildAccount(string accountCode, string accountHeadName, AccountHeadTypeEnum accountHeadType, AccountPeriodEnum accountPeriod, bool isLedger)
    {
      NOAccountHead accountHead = Container.NewTransientInstance<NOAccountHead>();
      accountHead.AccountCode = accountCode;
      accountHead.AccountHeadName = accountHeadName;
      accountHead.ParentHead = this;
      accountHead.AccountHeadType = accountHeadType;
      accountHead.AccountType = this.AccountType;
      accountHead.AccountPeriod = accountPeriod;
      accountHead.IsLedger = IsLedger;
      accountHead.Status = StatusEnum.Active;
      Container.Persist(ref accountHead);
    }
    public bool HideAddChildAccount()
    {
      if (this.IsLedger) return true;
      return false;
    }
    #endregion
  }
}
