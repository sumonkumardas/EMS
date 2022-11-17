using System;

namespace SinePulse.EMS.Messages.AttendanceMessages
{
  public class ShowStudentAttendanceRequestMessage : MediatR.IRequest<ShowStudentAttendanceResponseMessage>
  {
    public long StudentAttendanceId { get; set; }
  }
}