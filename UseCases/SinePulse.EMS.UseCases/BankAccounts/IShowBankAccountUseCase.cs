using SinePulse.EMS.Messages.BankAccountMessages;

namespace SinePulse.EMS.UseCases.BankAccounts
{
  public interface IShowBankAccountUseCase
  {
    ShowBankAccountResponseMessage.AccountInfo ShowBankAccount(ShowBankAccountRequestMessage message);
  }
}