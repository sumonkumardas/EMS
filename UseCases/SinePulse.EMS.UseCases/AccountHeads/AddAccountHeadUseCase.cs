using System;
using AutoMapper;
using SinePulse.EMS.Domain.Accounts;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.AccountHeadMessages;
using SinePulse.EMS.Messages.Model.Accounts;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.AccountHeads
{
  public class AddAccountHeadUseCase : IAddAccountHeadUseCase
  {
    private readonly IRepository _repository;
    private readonly EmsDbContext _dbContext;
    private readonly MapperConfiguration _autoMapperConfiguration;

    public AddAccountHeadUseCase(IRepository repository, EmsDbContext dbContext)
    {
      _repository = repository;
      _dbContext = dbContext;
      _autoMapperConfiguration = new MapperConfiguration(cfg => cfg.CreateMap<AccountHead, AccountHeadMessageModel>());
    }

    public AccountHeadMessageModel AddAccountHead(AddAccountHeadRequestMessage message)
    {
      var parentHead = _repository.GetById<AccountHead>(message.ParentHeadId);
      var accountHead = new AccountHead
      {
        AccountCode = message.AccountCode,
        AccountHeadName = message.AccountHeadName,
        AccountHeadType = message.AccountHeadType,
        AccountPeriod = message.AccountPeriod,
        ParentHead = parentHead,
        Status = message.Status,
        AuditFields = new AuditFields
        {
          InsertedBy = message.CurrentUserName,
          InsertedDateTime = DateTime.Now,
          LastUpdatedBy = message.CurrentUserName,
          LastUpdatedDateTime = DateTime.Now
        }
      };
      _repository.Insert(accountHead);
      _dbContext.SaveChanges();
      var mapper = _autoMapperConfiguration.CreateMapper();
      return mapper.Map(accountHead, new AccountHeadMessageModel());
    }
  }
}