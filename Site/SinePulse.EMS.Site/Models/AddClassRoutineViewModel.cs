using System;
using System.Collections.Generic;
using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Messages.Model.Employees;
using SinePulse.EMS.Messages.Model.Examinations;

namespace SinePulse.EMS.Site.Models
{
  public class AddClassRoutineViewModel: BaseViewModel
  {
    public DayOfWeek WeekDay { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public bool IsBreakTime { get; set; }
    public long SubjectId { get; set; }
    public long TeacherId { get; set; }
    public long SectionId { get; set; }
    public long RoomId { get; set; }
    public IEnumerable<RoomMessageModel> Rooms { get; set; }
    public IEnumerable<SubjectMessageModel> Subjects { get; set; }
    public IEnumerable<EmployeeMessageModel> Teachers { get; set; }
  }
}