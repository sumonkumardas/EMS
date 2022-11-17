using SinePulse.EMS.Messages.BalanceSheetMessages;

namespace SinePulse.EMS.UseCases.BalanceSheets
{
  public interface IShowBalanceSheetUseCase
  {
    ShowBalanceSheetResponseMessage ShowBalanceSheet(ShowBalanceSheetRequestMessage requestMessage);
  }
}