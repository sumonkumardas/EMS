using SinePulse.EMS.Messages.SalarySheetMessages;

namespace SinePulse.EMS.UseCases.SalarySheets
{
  public interface ISalarySheetAccountPostingUseCase
  {
    SalarySheetAccountPostingResponseMessage.AccountPostResponse PostSalarySheetAccount(
      SalarySheetAccountPostingRequestMessage message);
  }
}