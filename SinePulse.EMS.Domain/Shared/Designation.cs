using SinePulse.EMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SinePulse.EMS.Domain.Shared
{
  public class Designation : BaseEntity
  {
    #region Primitive Properties
    public virtual string DesignationName { get; set; }
    public virtual EmployeeTypeEnum EmployeeType { get; set; }
    public virtual StatusEnum Status { get; set; }
    #endregion

    #region Complex Properties
    private AuditFields _auditFields = new AuditFields();

    public virtual AuditFields AuditFields
    {
      get { return _auditFields; }
      set { _auditFields = value; }
    }
    #endregion
  }
}
