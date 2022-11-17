using AutoMapper;
using SinePulse.EMS.Domain.Attendance;
using SinePulse.EMS.Messages.AttendanceMessages;
using SinePulse.EMS.Messages.Model.Attendance;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Students
{
  public class ShowStudentAttendanceUseCase : IShowStudentAttendanceUseCase
  {
    private readonly IRepository _repository;
    private readonly MapperConfiguration _mapperConfiguration;

    public ShowStudentAttendanceUseCase(IRepository repository)
    {
      _repository = repository;
      _mapperConfiguration = new MapperConfiguration(cfg => cfg.CreateMap<Attendance, StudentAttendanceMessageModel>());
    }

    public StudentAttendanceMessageModel ShowStudentAttendance(ShowStudentAttendanceRequestMessage message)
    {
      var includes = new[] {nameof(Attendance.Student)};
      var studentAttendance = _repository.GetByIdWithInclude<Attendance>(message.StudentAttendanceId, includes);
      var mapper = _mapperConfiguration.CreateMapper();
      return mapper.Map(studentAttendance, new StudentAttendanceMessageModel());
    }
  }
}