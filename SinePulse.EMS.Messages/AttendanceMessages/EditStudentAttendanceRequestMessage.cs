using SinePulse.EMS.Domain.Enums;
using System;

namespace SinePulse.EMS.Messages.AttendanceMessages
{
  public class EditStudentAttendanceRequestMessage : MediatR.IRequest<EditStudentAttendanceResponseMessage>
  {
    public long Id { get; set; }
    public DateTime AttendanceDate { get; set; }
    public TimeSpan InTime { get; set; }
    public TimeSpan OutTime { get; set; }
    public long StudentId { get; set; }
    public string CurrentUserName { get; set; }
  }
}