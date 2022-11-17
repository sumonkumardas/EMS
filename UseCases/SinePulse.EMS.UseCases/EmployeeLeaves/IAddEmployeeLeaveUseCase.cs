using SinePulse.EMS.Messages.EmployeeLeaveMessages;

namespace SinePulse.EMS.UseCases.EmployeeLeaves
{
  public interface IAddEmployeeLeaveUseCase
  {
    void AddEmployeeLeave(AddEmployeeLeaveRequestMessage request);
  }
}