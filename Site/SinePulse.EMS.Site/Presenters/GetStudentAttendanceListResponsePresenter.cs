using SinePulse.EMS.Messages.AttendanceMessages;
using SinePulse.EMS.Site.Models;

namespace SinePulse.EMS.Site.Presenters
{
  public class GetStudentAttendanceListResponsePresenter
  {
    public ShowStudentAttendanceViewModel Handle(ShowCurrentMonthAttendanceListResponseMessage message, ShowStudentAttendanceViewModel viewModel)
    {
      viewModel.StudentAttendances = message.AttendanceList;
      return viewModel;
    }
  }
}