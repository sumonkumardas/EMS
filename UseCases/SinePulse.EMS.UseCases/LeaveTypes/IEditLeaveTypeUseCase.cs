using SinePulse.EMS.Messages.LeaveTypeMessages;

namespace SinePulse.EMS.UseCases.LeaveTypes
{
  public interface IEditLeaveTypeUseCase
  {
    void EditLeaveType(EditLeaveTypeRequestMessage message);
  }
}