using SinePulse.EMS.Messages.GenerateSalarySheetMessages;
using SinePulse.EMS.Messages.SalarySheetMessages;

namespace SinePulse.EMS.UseCases.SalarySheets
{
  public interface ISaveSalarySheetUseCase
  {
    void SaveSalarySheet(SaveSalarySheetRequestMessage message);
  }
}