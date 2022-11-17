using System;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Messages.Model.Shared;
using SinePulse.EMS.ProjectPrimitives;

namespace SinePulse.EMS.Messages.Model.Academic
{
  public class BreakTimeMessageModel : BaseEntityMessageModel
  {
    #region Primitive Properties

    public WeekDays WeekDays { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public StatusEnum Status { get; set; }

    #endregion

    #region  Navigation Properties

    public BranchMediumMessageModel Branch { get; set; }
    public SessionMessageModel Session { get; set; }

    #endregion
  }
}