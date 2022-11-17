using System.Collections.Generic;
using AutoMapper;
using SinePulse.EMS.Messages.AttendanceMessages;
using SinePulse.EMS.Messages.Model.Attendance;
using SinePulse.EMS.Messages.Model.Employees;
using SinePulse.EMS.Site.Models;

namespace SinePulse.EMS.Site.Presenters
{
  public class ShowEmployeeAttendanceListResponsePresenter
  {
    private MapperConfiguration _autoMapperConfig;

    public List<EmployeeAttendanceViewModel> Handle(ShowCurrentMonthAttendanceListResponseMessage message,
      List<EmployeeAttendanceViewModel> viewModel)
    {
      _autoMapperConfig = new MapperConfiguration(cfg =>
      {
        cfg.CreateMap<EmployeeMessageModel, EmployeeViewModel>();
        cfg.CreateMap<EmployeeAttendanceMessageModel, EmployeeAttendanceViewModel>();
      });
      var mapper = _autoMapperConfig.CreateMapper();
      viewModel = mapper.Map<List<EmployeeAttendanceViewModel>>(message.AttendanceList);
      return viewModel;
    }
  }
}