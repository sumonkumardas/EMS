using System;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Attendance;
using SinePulse.EMS.Domain.Employees;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.AttendanceMessages;
using SinePulse.EMS.Messages.Model.Attendance;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Employee
{
  public class AddEmployeeAttendanceUseCase : IAddEmployeeAttendanceUseCase
  {
    private readonly IRepository _repository;

    public AddEmployeeAttendanceUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public Attendance AddEmployeeAttendance(AddEmployeeAttendanceRequestMessage request)
    {
      var employee = _repository.GetById<Domain.Employees.Employee>(request.EmployeeId);

      var employeeAttendance = new Attendance();
      employeeAttendance.AttendanceDate = request.AttendanceDate;
      employeeAttendance.InTime = request.InTime;
      employeeAttendance.OutTime = request.OutTime;
      employeeAttendance.Employee = employee;
      employeeAttendance.AuditFields = new AuditFields()
      {
        InsertedBy = request.CurrentUserName,
        InsertedDateTime = DateTime.Now,
        LastUpdatedBy = request.CurrentUserName,
        LastUpdatedDateTime = DateTime.Now
      };

      _repository.Insert(employeeAttendance);
      return employeeAttendance;

    }
  }
}