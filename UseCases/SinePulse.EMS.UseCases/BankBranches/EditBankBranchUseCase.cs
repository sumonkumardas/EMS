using System;
using SinePulse.EMS.Domain.Banks;
using SinePulse.EMS.Messages.BankBranchMessages;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.BankBranches
{
  public class EditBankBranchUseCase : IEditBankBranchUseCase
  {
    private readonly IRepository _repository;
    private readonly EmsDbContext _dbContext;

    public EditBankBranchUseCase(IRepository repository, EmsDbContext dbContext)
    {
      _repository = repository;
      _dbContext = dbContext;
    }

    public void EditBankBranch(EditBankBranchRequestMessage message)
    {
      var bankBranch = _repository.GetById<BankBranch>(message.BankBranchId);
      bankBranch.Address = message.Address;
      bankBranch.BranchName = message.BranchName;
      bankBranch.AuditFields.LastUpdatedBy = message.CurrentUserName;
      bankBranch.AuditFields.LastUpdatedDateTime = DateTime.Now;

      _dbContext.SaveChanges();
    }
  }
}