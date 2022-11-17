using SinePulse.EMS.Messages.EmployeeMessages;

namespace SinePulse.EMS.UseCases.Employee
{
  public interface IAddEmployeeUseCase
  {
    Domain.Employees.Employee AddEmployee(AddEmployeeRequestMessage request);
  }
}