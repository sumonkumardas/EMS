using System.Collections.Generic;
using SinePulse.EMS.Domain.Banks;
using SinePulse.EMS.Domain.Employees;
using SinePulse.EMS.Domain.Examinations;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Domain.Students;
using SinePulse.EMS.ProjectPrimitives;

namespace SinePulse.EMS.Domain.Academic
{
  public class BranchMedium : BaseEntity
  {
    public override string Title()
    {
      return this.Branch.Title() + " -> " + this.Medium.Title();
    }

    #region Primitive Properties
    public virtual WeekDays WeeklyHolidays { get; set; }
    public int SessionBufferPeriods { get; set; }
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

    public virtual Branch Branch { get; set; }
    public virtual Medium Medium { get; set; }
    public virtual Shift Shift { get; set; }
    #endregion

    #region Collection Properties
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();
    public virtual ICollection<Section> Sections { get; set; } = new List<Section>();
    public virtual ICollection<Bank> Banks { get; set; } = new List<Bank>();
    
    #endregion

    #region Get Properties

    private ICollection<Student> _students = new List<Student>();

    public virtual ICollection<Student> Students
    {
      get { return _students; }
      set { _students = value; }
    }

    #endregion
  }
}