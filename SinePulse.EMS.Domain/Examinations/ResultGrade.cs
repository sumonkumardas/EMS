using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Shared;

namespace SinePulse.EMS.Domain.Examinations
{
  public class ResultGrade : BaseEntity
  {
    public override string Title()
    {
      return $"{GradeLetter} [{StartMark - EndMark}]";
    }

    #region Primitive Properties

    public virtual string GradeLetter { get; set; }

    public virtual decimal GradePoint { get; set; }

    public virtual int StartMark { get; set; }

    public virtual int EndMark { get; set; }

    public virtual StatusEnum Status { get; set; }

    #endregion

    #region Complex Properties

    public virtual AuditFields AuditFields { get; set; } = new AuditFields();

    #endregion

    #region  Navigation Properties

    public virtual BranchMedium BranchMedium { get; set; }

    #endregion
  }
}