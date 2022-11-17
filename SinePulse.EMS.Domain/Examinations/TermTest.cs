using System;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Shared;

namespace SinePulse.EMS.Domain.Examinations
{
  public class TermTest : BaseEntity
  {
    #region Primitive Properties

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

    #endregion
  }
}