using System;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Employees;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Leaves;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.EmployeeLeaveMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.EmployeeLeaves
{
  public class AddEmployeeLeaveUseCase : IAddEmployeeLeaveUseCase
  {
    private readonly IRepository _repository;

    public AddEmployeeLeaveUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public void AddEmployeeLeave(AddEmployeeLeaveRequestMessage request)
    {
      var includes = new string[] { nameof(Domain.Employees.Employee.ReportTo)};
      var leaveType = _repository.GetById<LeaveType>(request.LeaveTypeId);
      var employee = _repository.GetByIdWithInclude<Domain.Employees.Employee>(request.EmployeeId,includes);

      var employeeLeave = new EmployeeLeave
      {
        EndDate = request.EndDate,
        LeaveStatus = LeaveStatusEnum.Pending,
        Reason = request.Reason,
        StartDate = request.StartDate,
        LeaveType = leaveType,
        ApproveBy = employee.ReportTo,
        Employee = employee,
        AuditFields = new AuditFields
        {
          InsertedBy = request.CurrentUserName,
          InsertedDateTime = DateTime.Now,
          LastUpdatedBy = request.CurrentUserName,
          LastUpdatedDateTime = DateTime.Now
        }
      };
      
      _repository.Insert(employeeLeave);
    }
  }
}