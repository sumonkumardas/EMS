using SinePulse.EMS.Messages.BankAccountMessages;
using SinePulse.EMS.Messages.Model.Banks;

namespace SinePulse.EMS.UseCases.BankAccounts
{
  public interface IAddBankAccountUseCase
  {
    BankAccountMessageModel AddBankAccount(AddBankAccountRequestMessage message);
  }
}