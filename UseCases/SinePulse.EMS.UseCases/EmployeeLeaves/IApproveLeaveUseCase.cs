using SinePulse.EMS.Messages.EmployeeLeaveMessages;

namespace SinePulse.EMS.UseCases.EmployeeLeaves
{
  public interface IApproveLeaveUseCase
  {
    long ApproveLeave(ApproveLeaveRequestMessage request);
  }
}