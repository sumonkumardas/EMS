using SinePulse.EMS.Messages.SalarySheetMessages;

namespace SinePulse.EMS.UseCases.SalarySheets
{
  public interface IChangeButtonStatusUseCase
  {
    ChangeButtonStatusResponseMessage.ButtonStatus ChangeButtonStatus(ChangeButtonStatusRequestMessage message);
  }
}