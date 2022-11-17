using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SinePulse.EMS.Domain.Employees
{
  public class EmployeePersonalInfo : BaseEntity
  {
    #region Primitive Properties
    public virtual string FatherName { get; set; }
    public virtual string MotherName { get; set; }
    public virtual string SpouseName { get; set; }
    public virtual GenderEnum Gender { get; set; }
    public virtual ReligionEnum Religion { get; set; }
    public virtual BloodGroupEnum BloodGroup { get; set; }
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
    [Required]
    public virtual Employee Employee { get; set; }
    #endregion
  }
}
