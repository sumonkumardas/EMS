using System;
using MediatR;

namespace SinePulse.EMS.Messages.ClassRoutineMessages
{
  public class AddClassRoutineRequestMessage : IRequest<AddClassRoutineResponseMessage>
  {
    public DayOfWeek WeekDay { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public bool IsBreakTime { get; set; }
    public long SubjectId { get; set; }
    public long TeacherId { get; set; }
    public long SectionId { get; set; }
    public long RoomId { get; set; }
    public string CurrentUserName { get; set; }
  }
}