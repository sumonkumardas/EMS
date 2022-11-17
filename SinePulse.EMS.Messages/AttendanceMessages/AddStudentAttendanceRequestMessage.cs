using SinePulse.EMS.Domain.Enums;
using System;

namespace SinePulse.EMS.Messages.AttendanceMessages
{
  public class AddStudentAttendanceRequestMessage : MediatR.IRequest<AddStudentAttendanceResponseMessage>
  {
    public DateTime AttendanceDate { get; set; }
    public TimeSpan InTime { get; set; }
    public TimeSpan OutTime { get; set; }
    public long StudentId { get; set; }
    public string CurrentUserName { get; set; }
  }
}