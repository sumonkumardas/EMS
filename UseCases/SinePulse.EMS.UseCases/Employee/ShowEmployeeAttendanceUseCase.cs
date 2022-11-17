using AutoMapper;
using SinePulse.EMS.Domain.Attendance;
using SinePulse.EMS.Messages.AttendanceMessages;
using SinePulse.EMS.Messages.Model.Attendance;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Employee
{
  public class ShowEmployeeAttendanceUseCase : IShowEmployeeAttendanceUseCase
  {
    private readonly IRepository _repository;
    private readonly MapperConfiguration _autoMapperConfig;

    public ShowEmployeeAttendanceUseCase(IRepository repository)
    {
      _repository = repository;
      _autoMapperConfig = new MapperConfiguration(cfg =>
        cfg.CreateMap<Attendance, EmployeeAttendanceMessageModel>());
    }

    public EmployeeAttendanceMessageModel ShowEmployeeAttendance(ShowEmployeeAttendanceRequestMessage request)
    {
      var dbEmployeeAttendance = _repository.GetById<Attendance>(request.AttendanceId);
      var mapper = _autoMapperConfig.CreateMapper();
      return mapper.Map<EmployeeAttendanceMessageModel>(dbEmployeeAttendance);
    }
  }
}