using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Shared;

namespace SinePulse.EMS.Domain.Examinations
{
  public class ResultSheetEntry : BaseEntity
  {
    #region Primitive Properties

    public virtual string GradeLetter { get; set; }

    public virtual decimal GradePoint { get; set; }
    public virtual decimal SubjectiveMark { get; set; }
    public virtual decimal ObjectiveMark { get; set; }
    public virtual decimal PracticalMark { get; set; }
    public virtual decimal ClassTestMark { get; set; }

    public virtual decimal TotalMark { get; set; }
    public virtual StatusEnum Status { get; set; }

    #endregion

    #region Complex Properties

    public virtual AuditFields AuditFields { get; set; } = new AuditFields();

    #endregion

    #region  Navigation Properties

    public virtual ResultSheet ResultSheet { get; set; }
    public virtual ExamConfiguration ExamConfiguration { get; set; }

    #endregion
  }
}