using System;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Leaves;
using SinePulse.EMS.Messages.EmployeeLeaveMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.EmployeeLeaves
{
  public class ApproveLeaveUseCase : IApproveLeaveUseCase
  {
    private readonly IRepository _repository;

    public ApproveLeaveUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public long ApproveLeave(ApproveLeaveRequestMessage request)
    {
      var includes = new[] {nameof(EmployeeLeave.Employee)};
      var leave = _repository.GetByIdWithInclude<EmployeeLeave>(request.LeaveId, includes);

      leave.LeaveStatus = (LeaveStatusEnum) request.LeaveStatus;
      leave.AuditFields.LastUpdatedBy = request.CurrentUserName;
      leave.AuditFields.LastUpdatedDateTime = DateTime.Now;

      if (leave.Employee.ReportToId == null)
        return 0;
      return leave.Employee.ReportToId.Value;
    }
  }
}