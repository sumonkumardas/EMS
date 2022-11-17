using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Leaves;
using SinePulse.EMS.Messages.EmployeeLeaveMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.EmployeeLeaves
{
  public class GetPendingLeavesUseCase : IGetPendingLeavesUseCase
  {
    private readonly IRepository _repository;

    public GetPendingLeavesUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public IEnumerable<GetPendingLeavesResponseMessage.PendingLeave> GetPendingLeaves(
      GetPendingLeavesRequestMessage message)
    {
      var reportingEmployees = _repository.Table<Domain.Employees.Employee>()
        .Where(e => e.ReportTo.Id == message.EmployeeId)
        .ToList();

      var pendingLeavesOfReportingEmployee = GetPendingLeavesOfEmployees(reportingEmployees);
      return pendingLeavesOfReportingEmployee;
    }

    private IEnumerable<GetPendingLeavesResponseMessage.PendingLeave> GetPendingLeavesOfEmployees(
      List<Domain.Employees.Employee> reportingEmployees)
    {
      var requestedPendingLeaves = new List<GetPendingLeavesResponseMessage.PendingLeave>();
      foreach (var employee in reportingEmployees)
      {
        var pendingLeaves = _repository.Table<EmployeeLeave>()
          .Include(nameof(LeaveType))
          .Include(nameof(Domain.Employees.Employee))
          .Where(pl => pl.Employee.Id == employee.Id
                       && pl.LeaveStatus == LeaveStatusEnum.Pending);
        foreach (var pendingLeave in pendingLeaves)
        {
          requestedPendingLeaves.Add(new GetPendingLeavesResponseMessage.PendingLeave
          {
            PendingLeaveId = pendingLeave.Id,
            Reason = pendingLeave.Reason,
            StartDate = pendingLeave.StartDate,
            EndDate = pendingLeave.EndDate,
            LeaveTypeId = pendingLeave.LeaveType.Id,
            LeaveTypeName = pendingLeave.LeaveType.LeaveName,
            LeaveStatus = pendingLeave.LeaveStatus.ToString("G"),
            EmployeeId = pendingLeave.Employee.Id,
            EmployeeName = pendingLeave.Employee.FullName
          });
        }
      }

      return requestedPendingLeaves;
    }
  }
}