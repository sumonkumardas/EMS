using SinePulse.EMS.Messages.EmployeeMessages;
using System;
using System.Collections.Generic;
using System.Text;

namespace SinePulse.EMS.UseCases.Employee
{
  public interface IShowBranchMediumEmployeeListUseCase
  {
    List<ShowEmployeeListResponseMessage.Employee> ShowBranchMediumEmployeeList(ShowBranchMediumEmployeeListRequestMessage request);
  }
}
