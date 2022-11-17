using System.Collections.Generic;
using SinePulse.EMS.Messages.SalarySheetMessages;

namespace SinePulse.EMS.UseCases.SalarySheets
{
  public interface IGetSalarySheetUseCase
  {
    List<GetSalarySheetResponseMessage.EmployeeSalary> GetSalarySheet(GetSalarySheetRequestMessage message);
  }
}