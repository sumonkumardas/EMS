using SinePulse.EMS.Domain.Enums;
using System;

namespace SinePulse.EMS.Messages.AttendanceMessages
{
  public class AddEmployeeAttendanceRequestMessage : MediatR.IRequest<AddEmployeeAttendanceResponseMessage>
  {
    public DateTime AttendanceDate { get; set; }
    public TimeSpan InTime { get; set; }
    public TimeSpan OutTime { get; set; }
    public long EmployeeId { get; set; }
    public string CurrentUserName { get; set; }
  }
}