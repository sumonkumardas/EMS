using System;
using SinePulse.EMS.Domain.Leaves;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.LeaveTypeMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.LeaveTypes
{
  public class AddLeaveTypeUseCase : IAddLeaveTypeUseCase
  {
    private readonly IRepository _repository;

    public AddLeaveTypeUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public LeaveType AddLeaveType(AddLeaveTypeRequestMessage message)
    {
      var leaveCarriedForward = 0;
      if (message.WillLeaveCarriedForward && message.PercentageOfLeaveCarriedForward != null)
      {
        leaveCarriedForward = (int) message.PercentageOfLeaveCarriedForward;
      }
      var leaveType = new LeaveType
      {
        LeaveName = message.LeaveName,
        LeavesPerYear = message.LeavesPerYear,
        CanEmployeesApplyBeyondTheCurrentLeaveBalance = message.CanEmployeesApplyBeyondTheCurrentLeaveBalance,
        PercentageOfLeaveCarriedForward = leaveCarriedForward,
        Status = (Domain.Enums.StatusEnum)message.Status,
        WillLeaveCarriedForward = message.WillLeaveCarriedForward,
        AuditFields = new AuditFields
        {
          InsertedBy = message.CurrentUserName,
          InsertedDateTime = DateTime.Now,
          LastUpdatedBy = message.CurrentUserName,
          LastUpdatedDateTime = DateTime.Now
        }
      };
      _repository.Insert(leaveType);
      return leaveType;
    }
  }
}