using System;
using System.Collections.Generic;
using SinePulse.EMS.Domain.Employees;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Examinations;
using SinePulse.EMS.Domain.Shared;

namespace SinePulse.EMS.Domain.Academic
{
  public class Branch : BaseEntity
  {
    public override string Title()
    {
      return this.Institute.Title() + " -> " + this.BranchName;
    }

    #region Primitive Properties

    public virtual string BranchName { get; set; }
    public virtual string BranchCode { get; set; }
    public virtual string MobileNo { get; set; }
    public virtual string Email { get; set; }
    public virtual string Fax { get; set; }
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

    #region  Navigation Properties
    public virtual Institute Institute { get; set; }
    public virtual Address Address { get; set; }
    #endregion

    #region Collection Properties
    public virtual ICollection<BranchMedium> Mediums { get; set; } = new List<BranchMedium>();
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();
    public virtual ICollection<Shift> Shifts { get; set; } = new List<Shift>();
    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
    #endregion
  }
}