using FluentValidation;
using SinePulse.EMS.Messages.AttendanceMessages;

namespace SinePulse.EMS.UseCases.Attendances
{
  public class GetAttendanceListByDateRangeRequestMessageValidator : AbstractValidator<
    GetAttendanceListByDateRangeRequestMessage>
  {
  }
}