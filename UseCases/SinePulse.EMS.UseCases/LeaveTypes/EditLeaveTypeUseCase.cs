using System;
using SinePulse.EMS.Domain.Leaves;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.LeaveTypeMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.LeaveTypes
{
  public class EditLeaveTypeUseCase : IEditLeaveTypeUseCase
  {
    private readonly IRepository _repository;

    public EditLeaveTypeUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public void EditLeaveType(EditLeaveTypeRequestMessage message)
    {
      var leaveCarriedForward = 0;
      if (message.WillLeaveCarriedForward && message.PercentageOfLeaveCarriedForward != null)
      {
        leaveCarriedForward = (int) message.PercentageOfLeaveCarriedForward;
      }
      var leaveType = _repository.GetById<LeaveType>(message.Id);
      leaveType.LeaveName = message.LeaveName;
      leaveType.LeavesPerYear = message.LeavesPerYear;
      leaveType.CanEmployeesApplyBeyondTheCurrentLeaveBalance = message.CanEmployeesApplyBeyondTheCurrentLeaveBalance;
      leaveType.PercentageOfLeaveCarriedForward = leaveCarriedForward;
      leaveType.Status = (Domain.Enums.StatusEnum)message.Status;
      leaveType.WillLeaveCarriedForward = message.WillLeaveCarriedForward;
      leaveType.AuditFields.LastUpdatedBy = message.CurrentUserName;
      leaveType.AuditFields.LastUpdatedDateTime = DateTime.Now;
    }
  }
}