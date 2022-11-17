using System;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Shared;

namespace SinePulse.EMS.Domain.Examinations
{
  public class ExamRoutine : BaseEntity
  {
    #region Primitive Properties

    public virtual string ExamRoutineName { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
    public virtual StatusEnum Status { get; set; }

    #endregion

    #region Complex Properties

    public virtual AuditFields AuditFields { get; set; } = new AuditFields();

    #endregion

    #region  Navigation Properties

    public virtual ClassSubject ClassSubject { get; set; }
    public virtual ExamTerm Term { get; set; }
    #endregion
  }
}