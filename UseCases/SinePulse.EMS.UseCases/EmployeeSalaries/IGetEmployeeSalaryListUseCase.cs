using System.Collections.Generic;
using SinePulse.EMS.Messages.EmployeeSalaryMessages;

namespace SinePulse.EMS.UseCases.EmployeeSalaries
{
  public interface IGetEmployeeSalaryListUseCase
  {
    List<GetEmployeeSalaryListResponseMessage.EmployeeSalary> GetEmployeeSalaryList(
      GetEmployeeSalaryListRequestMessage message);
  }
}