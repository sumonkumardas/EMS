using System;
using SinePulse.EMS.Domain.Attendance;
using SinePulse.EMS.Domain.Students;
using SinePulse.EMS.Messages.AttendanceMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Students
{
  public class EditStudentAttendanceUseCase : IEditStudentAttendanceUseCase
  {
    private readonly IRepository _repository;

    public EditStudentAttendanceUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public void EditStudentAttendance(EditStudentAttendanceRequestMessage request)
    {
      var studentAttendance = _repository.GetById<Attendance>(request.Id);
      studentAttendance.AttendanceDate = request.AttendanceDate;
      studentAttendance.InTime = request.InTime;
      studentAttendance.OutTime = request.OutTime;

      studentAttendance.AuditFields.LastUpdatedBy = request.CurrentUserName;
      studentAttendance.AuditFields.LastUpdatedDateTime = DateTime.Now;

    }
  }
}