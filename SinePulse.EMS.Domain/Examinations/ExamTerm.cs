using System;
using System.Collections.Generic;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Shared;

namespace SinePulse.EMS.Domain.Examinations
{
  /**
   * First Term
   * Second Term
   * Final
   */
  public class ExamTerm : BaseEntity
  {
    public override string Title()
    {
      return $"{TermName} [{Session.Title()}]";
    }

    #region Primitive Properties

    public virtual string TermName { get; set; }

    public virtual DateTime StartDate { get; set; }

    public virtual DateTime EndDate { get; set; }

    public virtual StatusEnum Status { get; set; }

    #endregion

    #region Complex Properties

    public virtual AuditFields AuditFields { get; set; } = new AuditFields();

    #endregion

    #region  Navigation Properties
    public virtual Session Session { set; get; }
    #endregion

    #region Collection Properties
    public virtual ICollection<ExamConfiguration> ExamConfigurations { get; set; } = new List<ExamConfiguration>();
    #endregion
  }
}