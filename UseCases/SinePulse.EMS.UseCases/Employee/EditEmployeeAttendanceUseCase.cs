using System;
using SinePulse.EMS.Domain.Attendance;
using SinePulse.EMS.Messages.AttendanceMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Employee
{
  public class EditEmployeeAttendanceUseCase : IEditEmployeeAttendanceUseCase
  {
    private readonly IRepository _repository;

    public EditEmployeeAttendanceUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public void EditEmployeeAttendance(EditEmployeeAttendanceRequestMessage request)
    {
      var employeeAttendance = _repository.GetById<Attendance>(request.Id);
      employeeAttendance.AttendanceDate = request.AttendanceDate;
      employeeAttendance.InTime = request.InTime;
      employeeAttendance.OutTime = request.OutTime;


      employeeAttendance.AuditFields.LastUpdatedBy = request.CurrentUserName;
      employeeAttendance.AuditFields.LastUpdatedDateTime = DateTime.Now;
    }
  }
}