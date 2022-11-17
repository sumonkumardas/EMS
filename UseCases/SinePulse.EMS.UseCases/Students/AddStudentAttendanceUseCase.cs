using System;
using SinePulse.EMS.Domain.Attendance;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.AttendanceMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Students
{
  public class AddStudentAttendanceUseCase : IAddStudentAttendanceUseCase
  {
    private readonly IRepository _repository;

    public AddStudentAttendanceUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public Attendance AddStudentAttendance(AddStudentAttendanceRequestMessage request)
    {
      var student = _repository.GetById<Domain.Students.Student>(request.StudentId);

      var studentAttendance = new Attendance();
      studentAttendance.AttendanceDate = request.AttendanceDate;
      studentAttendance.InTime = request.InTime;
      studentAttendance.OutTime = request.OutTime;
      studentAttendance.Student = student;
      studentAttendance.AuditFields = new AuditFields()
      {
        InsertedBy = request.CurrentUserName,
        InsertedDateTime = DateTime.Now,
        LastUpdatedBy = request.CurrentUserName,
        LastUpdatedDateTime = DateTime.Now
      };

      _repository.Insert(studentAttendance);
      return studentAttendance;

    }
  }
}