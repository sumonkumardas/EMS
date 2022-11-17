using NakedObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinePulse.EMS.NO.Domain.Shared
{
  [Table("Addresses")]
  [DisplayName("Address")]
  public class NOAddress : NOBaseEntity
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
    [MemberOrder(10), Required, Title]
    [StringLength(250)]
    [RegEx(Validation = @"^[^<>%$]*$", Message = "Invalid Street 1")]
    public virtual string Street1 { get; set; }

    [MemberOrder(20), Optionally]
    [StringLength(250)]
    [RegEx(Validation = @"^[^<>%$]*$", Message = "Invalid Street 2")]
    public virtual string Street2 { get; set; }

    [MemberOrder(30), Optionally]
    [RegEx(Validation = @"^[^<>%$]*$", Message = "Invalid Postal Code")]
    public virtual string PostalCode { get; set; }

    [MemberOrder(40), Optionally]
    [StringLength(150)]
    [RegEx(Validation = @"^[^<>%$]*$", Message = "Invalid City")]
    public virtual string City { get; set; }

    #endregion

    #region Complex Properties

    #region AuditFields (AuditFields)

    private AuditFields _auditFields = new AuditFields();

    [MemberOrder(250)]
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

    #endregion

    #region Edit Address Enable Disable

    public string DisablePropertyDefault()
    {
       return "You do not have permission to Edit";
    }

    #endregion
  }
}
