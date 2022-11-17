using System.Collections.Generic;
using SinePulse.EMS.Messages.GenerateSalarySheetMessages;

namespace SinePulse.EMS.UseCases.GenerateSalarySheets
{
  public interface IGetSalarySheetYearsUseCase
  {
    List<int> GetSalarySheetYears(GetSalarySheetYearsRequestMessage message);
  }
}