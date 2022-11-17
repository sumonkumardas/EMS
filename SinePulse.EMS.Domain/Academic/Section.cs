using System.Collections.Generic;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Examinations;
using SinePulse.EMS.Domain.Routines;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Domain.Students;

namespace SinePulse.EMS.Domain.Academic
{
  public class Section : BaseEntity
  {
    public override string Title()
    {
      return BranchMedium.Title() + " -> " + Class.Title() + " -> " + Group + " -> " + SectionName;
    }

    #region Primitive Properties

    public virtual GroupEnum Group { get; set; }
    public virtual string SectionName { get; set; }
    public virtual int NumberOfStudents { get; set; }
    public virtual int TotalClasses { get; set; }
    public virtual int DurationOfClass { get; set; }
    public virtual long ClassId { get; set; }
    public virtual long BranchMediumId { get; set; }
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

    public virtual Class Class { get; set; }
    public virtual BranchMedium BranchMedium { get; set; }
    public virtual Session Session { get; set; }
    public virtual Room Room { get; set; }

    #endregion

    #region Get Properties

    public virtual ICollection<ClassRoutine> Routines { get; } = new List<ClassRoutine>();
    public virtual ICollection<StudentSection> StudentSections { get; } = new List<StudentSection>();
    public virtual ICollection<ClassTest> ClassTests { get; } = new List<ClassTest>();

    #endregion
  }
}