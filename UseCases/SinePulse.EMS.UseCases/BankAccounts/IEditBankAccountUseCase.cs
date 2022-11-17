using SinePulse.EMS.Messages.BankAccountMessages;

namespace SinePulse.EMS.UseCases.BankAccounts
{
  public interface IEditBankAccountUseCase
  {
    void EditBankAccount(EditBankAccountRequestMessage message);
  }
}