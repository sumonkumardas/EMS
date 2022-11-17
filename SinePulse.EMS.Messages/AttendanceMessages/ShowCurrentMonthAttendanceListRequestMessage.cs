using System;
using MediatR;

namespace SinePulse.EMS.Messages.AttendanceMessages
{
  public class ShowCurrentMonthAttendanceListRequestMessage : IRequest<ShowCurrentMonthAttendanceListResponseMessage>
  {
    public DateTime AttendanceStartDate { get; set; }
    public DateTime AttendanceEndDate { get; set; }
    public long EmployeeId { get; set; }
    public long StudentId { get; set; }
  }
}