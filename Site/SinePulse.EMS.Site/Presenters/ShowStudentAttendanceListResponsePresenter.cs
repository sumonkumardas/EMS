using System.Collections.Generic;
using AutoMapper;
using System.Linq;
using SinePulse.EMS.Site.Models;
using SinePulse.EMS.Messages.AttendanceMessages;
using SinePulse.EMS.Messages.Model.Attendance;

namespace SinePulse.EMS.Site.Presenters
{
  public class ShowStudentAttendanceListResponsePresenter
  {
    private MapperConfiguration _autoMapperConfig;

    public List<StudentAttendanceViewModel> Handle(ShowCurrentMonthAttendanceListResponseMessage message, List<StudentAttendanceViewModel> viewModel)
    {
      _autoMapperConfig = new MapperConfiguration(cfg => { cfg.CreateMap<StudentAttendanceMessageModel, StudentAttendanceViewModel>(); });
      var mapper = _autoMapperConfig.CreateMapper();
      viewModel = mapper.Map(message.AttendanceList, new List<StudentAttendanceViewModel>());
      return viewModel;
    }
  }
}