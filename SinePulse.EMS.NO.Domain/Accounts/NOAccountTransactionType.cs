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
  [Table("AccountTransactionTypes")]
  [DisplayName("Account Transaction Type")]
  [Bounded]
  public class NOAccountTransactionType: NOBaseEntity
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

      string title = this.AccountType.Title() + " - " + this.AccountTransactionType.ToString();
      title = title + " - " + IncreaseDecreaseType.ToString() + ")";

      t.Append(title);

      return t.ToString();
    }

    #region Primitive Properties
    [Required, MemberOrder(10)]
    public virtual AccountTransactionTypeEnum AccountTransactionType { get; set; }
    [Required, MemberOrder(20)]
    public virtual IncreaseDecreaseEnum IncreaseDecreaseType { get; set; }
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
    [Required, MemberOrder(50)]
    public virtual NOAccountType AccountType { get; set; }
    #endregion
  }
}
