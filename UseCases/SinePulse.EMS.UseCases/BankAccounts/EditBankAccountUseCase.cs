using System;
using SinePulse.EMS.Domain.Banks;
using SinePulse.EMS.Messages.BankAccountMessages;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.BankAccounts
{
  public class EditBankAccountUseCase : IEditBankAccountUseCase
  {
    private readonly IRepository _repository;
    private readonly EmsDbContext _dbContext;

    public EditBankAccountUseCase(IRepository repository, EmsDbContext dbContext)
    {
      _repository = repository;
      _dbContext = dbContext;
    }

    public void EditBankAccount(EditBankAccountRequestMessage message)
    {
      var bankAccount = _repository.GetById<BankAccount>(message.BankAccountId);
      bankAccount.AccountNumber = message.AccountNumber;
      bankAccount.AccountType = message.AccountType;
      bankAccount.AuditFields.LastUpdatedBy = message.CurrentUserName;
      bankAccount.AuditFields.LastUpdatedDateTime = DateTime.Now;
      _dbContext.SaveChanges();
    }
  }
}