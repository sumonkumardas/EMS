using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Shared;

namespace SinePulse.EMS.Domain.Examinations
{
  public class ExamConfiguration : BaseEntity
  {
    #region Primitive Properties
    public virtual int SubjectiveFullMark { get; set; }
    public virtual int SubjectivePassMark { get; set; }
    public virtual int ObjectiveFullMark { get; set; }
    public virtual int ObjectivePassMark { get; set; }
    public virtual int PracticalFullMark { get; set; }
    public virtual int PracticalPassMark { get; set; }
    public virtual int ClassTestPercentage { get; set; }
    public virtual StatusEnum Status { get; set; }
    #endregion

    #region Complex Properties

    public virtual AuditFields AuditFields { get; set; } = new AuditFields();

    #endregion

    #region  Navigation Properties
    public virtual ExamTerm Term { get; set; }
    //public virtual ClassSubject ClassSubject { get; set; }
    public virtual Class Class { get; set; }
    public virtual Subject Subject { get; set; }
    #endregion
  }
}
