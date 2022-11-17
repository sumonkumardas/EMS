using SinePulse.EMS.Domain.Enums;
using System;

namespace SinePulse.EMS.Messages.AttendanceMessages
{
  public class ShowEmployeeAttendanceRequestMessage : MediatR.IRequest<ShowEmployeeAttendanceResponseMessage>
  {
    public long AttendanceId { get; set; }
  }
}