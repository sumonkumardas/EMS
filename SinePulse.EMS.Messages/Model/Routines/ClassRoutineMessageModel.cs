using System;
using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Messages.Model.Employees;
using SinePulse.EMS.Messages.Model.Enums;
using SinePulse.EMS.Messages.Model.Examinations;
using SinePulse.EMS.Messages.Model.Shared;

namespace SinePulse.EMS.Messages.Model.Routines
{
  public class ClassRoutineMessageModel : BaseEntityMessageModel
  {
    #region Primitive Properties

    public DayOfWeek WeekDay { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public bool IsBreakTime { get; set; }
    public StatusEnum Status { get; set; }

    #endregion

    #region  Navigation Properties

    public SubjectMessageModel Subject { get; set; }
    public EmployeeMessageModel Teacher { get; set; }
    public SectionMessageModel Section { get; set; }
    public RoomMessageModel Room { get; set; }

    #endregion
  }
}
