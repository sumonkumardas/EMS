using System;
using System.Collections.Generic;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Attendance;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Shared;

namespace SinePulse.EMS.Domain.Students
{
  public class Student : BaseEntity
  {
    public override string Title()
    {
      return FullName + " (" + StudentId + ")";
    }

    #region Primitive Properties

    public virtual string FullName { get; set; }
    public virtual string StudentId { get; set; }
    public virtual RelationTypeEnum Guardian { get; set; }
    public virtual DateTime BirthDate { get; set; }
    public virtual string Email { get; set; }
    public virtual string MobileNo { get; set; }
    public virtual StatusEnum Status { get; set; }
    public virtual string ImageUrl { get; set; }

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

    public virtual Address PresentAddress { get; set; }
    public virtual Address PermanentAddress { get; set; }
    public BranchMedium BranchMedium { get; set; }
    public Session Session { get; set; }

    #endregion

    #region Get Properties

    public ICollection<ContactPerson> ContactPersons { get; set; }

    #endregion
  }
}