using System.Collections.Generic;
using SinePulse.EMS.Messages.EmployeeMessages;

namespace SinePulse.EMS.UseCases.Employee
{
  public interface IShowEmployeeListUseCase
  {
    List<ShowEmployeeListResponseMessage.Employee> ShowEmployeeList(ShowEmployeeListRequestMessage request);
  }
}