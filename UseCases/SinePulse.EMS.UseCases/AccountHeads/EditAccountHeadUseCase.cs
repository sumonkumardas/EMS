using System;
using SinePulse.EMS.Domain.Accounts;
using SinePulse.EMS.Messages.AccountHeadMessages;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.AccountHeads
{
  public class EditAccountHeadUseCase : IEditAccountHeadUseCase
  {
    private readonly IRepository _repository;
    private readonly EmsDbContext _dbContext;

    public EditAccountHeadUseCase(IRepository repository, EmsDbContext dbContext)
    {
      _repository = repository;
      _dbContext = dbContext;
    }

    public void UpdateAccountHead(EditAccountHeadRequestMessage message)
    {
      var parentHead = _repository.GetById<AccountHead>(message.ParentHeadId);
      var accountHead = _repository.GetById<AccountHead>(message.AccountHeadId);

      accountHead.AccountCode = message.AccountCode;
      accountHead.AccountHeadName = message.AccountHeadName;
      accountHead.AccountPeriod = message.AccountPeriod;
      accountHead.ParentHead = parentHead;
      accountHead.AccountHeadType = message.AccountHeadType;
      accountHead.Status = message.Status;
      accountHead.AuditFields.LastUpdatedBy = message.CurrentUserName;
      accountHead.AuditFields.LastUpdatedDateTime = DateTime.Now;

      _dbContext.SaveChanges();
    }
  }
}