using SinePulse.EMS.Messages.TrialBalanceMessages;

namespace SinePulse.EMS.UseCases.TrialBalances
{
  public interface IShowTrialBalanceUseCase
  {
    ShowTrialBalanceResponseMessage ShowTrialBalance(ShowTrialBalanceRequestMessage requestMessage);
  }
}