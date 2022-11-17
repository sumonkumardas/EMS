using System;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Messages.Model.Shared;

namespace SinePulse.EMS.Messages.Model.Academic
{
  public class ShiftMessageModel : BaseEntityMessageModel
  {
    #region Primitive Properties

    public string ShiftName { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public StatusEnum Status { get; set; }

    #endregion

    #region  Navigation Properties

    public BranchMessageModel Branch { get; set; }

    #endregion
  }
}