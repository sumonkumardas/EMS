using SinePulse.EMS.Messages.Model.Employees;
using SinePulse.EMS.Messages.EmployeeMessages;

namespace SinePulse.EMS.UseCases.Employee
{
  public interface IShowEmployeeUseCase
  {
    Messages.Model.Employees.EmployeeMessageModel ShowEmployee(ShowEmployeeRequestMessage request);
  }
}