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
  [Table("AccountTypes")]
  [DisplayName("Account Type")]
  [Bounded]
  public class NOAccountType : NOBaseEntity
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

      string title = this.AccountTypeName.ToString() + " (" + this.CodingStartValue ;
      title = title + " - " + CodingEndValue.ToString() + ")";
      
      t.Append(title);

      return t.ToString();
    }

    #region Primitive Properties
    [Required, MemberOrder(10)]
    public virtual ChartOfAccountTypeEnum AccountTypeName { get; set; }
    [Required, MemberOrder(20)]
    public virtual FinancialStatementsEnum FinancialStatement { get; set; }
    [Required, MemberOrder(30)]
    public virtual int CodingStartValue { get; set; }
    [Required, MemberOrder(40)]
    public virtual int CodingEndValue { get; set; }
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

    #region Get Properties
    [NotMapped]
    [Eagerly(EagerlyAttribute.Do.Rendering)]
    [TableView(false, "AccountTransactionType", "IncreaseDecreaseType")]
    public IList<NOAccountTransactionType> FinancialTransactions
    {
      get
      {
        return Container.Instances<NOAccountTransactionType>().Where(w => w.AccountType.Id == this.Id).ToList();
      }
    }
    #endregion
  }
}
