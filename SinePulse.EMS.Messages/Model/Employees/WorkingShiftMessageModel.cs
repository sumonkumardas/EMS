using SinePulse.EMS.Messages.Model.Enums;
using SinePulse.EMS.Messages.Model.Shared;
using System;

namespace SinePulse.EMS.Messages.Model.Employees
{
  public class WorkingShiftMessageModel : BaseEntityMessageModel
  {
    #region Primitive Properties

    public string ShiftName { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public StatusEnum Status { get; set; }

    #endregion
  }
}
