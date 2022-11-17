using System;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Shared;

namespace SinePulse.EMS.Domain.Examinations
{
  public class ClassTest : BaseEntity
  {
    #region Primitive Properties

    public virtual string TestName { get; set; }
    public virtual decimal FullMarks { get; set; }
    public virtual DateTime StartTimestamp { get; set; }
    public virtual DateTime EndTimestamp { get; set; }
    public ExamTypeEnum ExamType { get; set; }
    public virtual StatusEnum Status { get; set; }

    #endregion

    #region Complex Properties

    public virtual AuditFields AuditFields { get; set; } = new AuditFields();

    #endregion

    #region  Navigation Properties

    public virtual ExamConfiguration ExamConfiguration { get; set; }
    public virtual Section Section { get; set; }
    public virtual Room Room { get; set; }

    #endregion
  }
}