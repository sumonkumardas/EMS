using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using SinePulse.EMS.Domain.Accounts;

namespace SinePulse.EMS.Domain.Academic
{
  public class Class: BaseEntity
  {
    public override string Title()
    {
      return this.ClassName + " (" + this.ClassCode + ")";
    }

    #region Primitive Properties
    public virtual string ClassName { get; set; }
    public virtual int ClassCode { get; set; }
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

    #region Get Properties

    public ICollection<AcademicFee> AcademicFees { get; set; }

    #endregion
  }
}
