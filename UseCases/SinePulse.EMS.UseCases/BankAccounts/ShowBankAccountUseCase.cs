using SinePulse.EMS.Domain.Banks;
using SinePulse.EMS.Messages.BankAccountMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.BankAccounts
{
  public class ShowBankAccountUseCase : IShowBankAccountUseCase
  {
    private readonly IRepository _repository;

    public ShowBankAccountUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public ShowBankAccountResponseMessage.AccountInfo ShowBankAccount(ShowBankAccountRequestMessage message)
    {
      var bankAccount = _repository.GetByIdWithInclude<BankAccount>(message.BankAccountId, new []{nameof(BankBranch)});
      return new ShowBankAccountResponseMessage.AccountInfo
      {
        AccountNo = bankAccount.AccountNumber,
        AccountType = bankAccount.AccountType,
        BankBranchId = bankAccount.BankBranch.Id
      };
    }
  }
}