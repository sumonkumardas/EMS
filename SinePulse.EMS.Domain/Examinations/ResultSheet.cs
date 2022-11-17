using System.Collections.Generic;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Domain.Students;

namespace SinePulse.EMS.Domain.Examinations
{
  public class ResultSheet : BaseEntity
  {
    #region Primitive Properties

    public virtual decimal TotalMark { get; set; }
    public virtual decimal Gpa { get; set; }
    public virtual string GradeLetter { get; set; }
    public virtual long TermId { get; set; }
    public virtual long StudentSectionId { get; set; }
    public virtual StatusEnum Status { get; set; }

    #endregion

    #region Complex Properties

    public virtual AuditFields AuditFields { get; set; } = new AuditFields();

    #endregion

    #region  Collection Properties

    public virtual ICollection<ResultSheetEntry> ResultSheetEntries { get; set; }

    #endregion

    #region  Navigation Properties

    public virtual ExamTerm Term { get; set; }
    public virtual StudentSection StudentSection { get; set; }

    #endregion
  }
}