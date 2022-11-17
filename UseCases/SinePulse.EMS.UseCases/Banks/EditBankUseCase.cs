using System;
using SinePulse.EMS.Domain.Banks;
using SinePulse.EMS.Messages.BankMessages;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Banks
{
  public class EditBankUseCase : IEditBankUseCase
  {
    private readonly IRepository _repository;
    private readonly EmsDbContext _dbContext;

    public EditBankUseCase(IRepository repository, EmsDbContext dbContext)
    {
      _repository = repository;
      _dbContext = dbContext;
    }

    public long EditBank(EditBankRequestMessage message)
    {
      var bank = _repository.GetById<Bank>(message.BankId);
      bank.BankName = message.BankName;
      bank.AuditFields.LastUpdatedBy = message.CurrentUserName;
      bank.AuditFields.LastUpdatedDateTime = DateTime.Now;
      _dbContext.SaveChanges();
      return bank.Id;
    }
  }
}