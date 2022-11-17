using SinePulse.EMS.Messages.EmployeeMessages;

namespace SinePulse.EMS.UseCases.Employee
{
  public interface IEditEmployeeUseCase
  {
    void EditEmployee(EditEmployeeRequestMessage request);
  }
}