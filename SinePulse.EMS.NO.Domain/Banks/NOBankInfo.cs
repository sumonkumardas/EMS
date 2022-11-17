using NakedObjects;
using SinePulse.EMS.NO.Domain.Enums;
using SinePulse.EMS.NO.Domain.Employees;
using SinePulse.EMS.NO.Domain.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SinePulse.EMS.NO.Domain.Academic;
using SinePulse.EMS.NO.Domain.Accounts;

namespace SinePulse.EMS.NO.Domain.Banks
{
  [Table("BankInfos")]
  [DisplayName("Bank Info")]
  [Bounded]
  public class NOBankInfo : NOBaseEntity
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

    #region Primitive Properties
    [Required, StringLength(250), MemberOrder(10)]
    [RegEx(Validation = @"^[^<>%$]*$", Message = "Bank Name")]
    public virtual string BankName { get; set; }
    [Required, StringLength(250), MemberOrder(20)]
    [RegEx(Validation = @"^[^<>%$]*$", Message = "Branch Name")]
    public virtual string BranchName { get; set; }
    [Optionally, StringLength(250), MemberOrder(30), Title]
    [RegEx(Validation = @"^[^<>%$]*$", Message = "Invalid Account Number")]
    public virtual string AccountNumber { get; set; }
    #endregion

    #region Complex Properties
    private AuditFields _auditFields = new AuditFields();
    [Required]
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

    #region Chart of Accounts
    [NotMapped, MemberOrder(20)]
    public NOAccountHead AccountHead
    {
      get
      {
        return Container.Instances<NOBankAccountHead>().Where(w => w.Bank.Id == this.Id).Select(s=>s.AccountHead).FirstOrDefault();
      }
    }
    #endregion

    #region  Navigation Properties
    [MemberOrder(100), Required]
    public virtual BranchMedium BranchMedium { get; set; }
    #endregion
  }
}
