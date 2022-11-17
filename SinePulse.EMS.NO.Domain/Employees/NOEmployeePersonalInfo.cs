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

namespace SinePulse.EMS.NO.Domain.Employees
{
  [Table("EmployeePersonalInfos")]
  [DisplayName("Personal Info")]
  public class NOEmployeePersonalInfo : NOBaseEntity
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
    [RegEx(Validation = @"^[^<>%$]*$", Message = "Invalid Father's Name")]
    public virtual string FatherName { get; set; }
    [Required, StringLength(250), MemberOrder(20)]
    [RegEx(Validation = @"^[^<>%$]*$", Message = "Invalid Mother's Name")]
    public virtual string MotherName { get; set; }
    [Optionally, StringLength(250), MemberOrder(30)]
    [RegEx(Validation = @"^[^<>%$]*$", Message = "Invalid Spouse Name")]
    public virtual string SpouseName { get; set; }
    [Required, MemberOrder(40)]
    public virtual GenderEnum Gender { get; set; }
    [Required, MemberOrder(50)]
    public virtual ReligionEnum Religion { get; set; }
    [MemberOrder(60), Required]
    public virtual BloodGroupEnum BloodGroup { get; set; }
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

    #region  Navigation Properties
    [MemberOrder(100), Required]
    public virtual NOEmployee Employee { get; set; }
    #endregion
  }
}
