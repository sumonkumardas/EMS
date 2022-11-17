using SinePulse.EMS.Domain.Leaves;
using SinePulse.EMS.Messages.LeaveTypeMessages;

namespace SinePulse.EMS.UseCases.LeaveTypes
{
  public interface IAddLeaveTypeUseCase
  {
    LeaveType AddLeaveType(AddLeaveTypeRequestMessage message);
  }
}