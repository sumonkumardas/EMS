using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Accounts;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.BranchMediumMessages;
using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Messages.Model.Accounts;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.BranchMediums
{
  public class ImportCOAFromMasterUseCase : IImportCOAFromMasterUseCase
  {
    private readonly IRepository _repository;
    private readonly MapperConfiguration _accountTypeMapperConfiguration;
    private readonly MapperConfiguration _sessionMapperConfiguration;
    private readonly MapperConfiguration _accountHeadMapperConfiguration;
    private readonly EmsDbContext _dbContext;

    public ImportCOAFromMasterUseCase(IRepository repository, EmsDbContext dbContext)
    {
      _repository = repository;
      _dbContext = dbContext;
      _accountTypeMapperConfiguration =
        new MapperConfiguration(cnf => cnf.CreateMap<AccountTypeMessageModel, AccountType>());
      _sessionMapperConfiguration = new MapperConfiguration(cnf => cnf.CreateMap<SessionMessageModel, Session>());
      _accountHeadMapperConfiguration =
        new MapperConfiguration(cnf => cnf.CreateMap<AccountHeadMessageModel, AccountHead>());
    }

    public void ImportCOAFromMaster(ImportCOAFromMasterRequestMessage message)
    {
      var branchMedium = _repository.GetById<BranchMedium>(message.BranchMediumId);
      var session = _repository.GetById<Session>(message.SessionId);
      var accountTypeMapper = _accountTypeMapperConfiguration.CreateMapper();
      var sessionMapper = _sessionMapperConfiguration.CreateMapper();
      var accountHeadMapper = _accountHeadMapperConfiguration.CreateMapper();
      List<BranchMediumAccountHead> branchMediumAccountsHeadList = new List<BranchMediumAccountHead>();
      foreach (var accountsHead in message.AccountsHeads)
      {
        var parentHead = new BranchMediumAccountHead();

        if (accountsHead.ParentHead != null)
        {
          parentHead = branchMediumAccountsHeadList.Where(x => x.AccountCode == accountsHead.ParentHead.AccountCode && x.AccountHeadName == accountsHead.ParentHead.AccountHeadName && x.AccountType.Id == accountsHead.ParentHead.AccountType.Id).SingleOrDefault();
        }
        else
          parentHead = null;
        var branchMediumAccountHead = new BranchMediumAccountHead
        {
          AccountCode = accountsHead.AccountCode,
          AccountHeadName = accountsHead.AccountHeadName,
          AccountHeadType = accountsHead.AccountHeadType,
          AccountPeriod = accountsHead.AccountPeriod,
          AccountType = accountTypeMapper.Map(accountsHead.AccountType, new AccountType()),
          Status = accountsHead.Status,
          ParentHead = parentHead,
          IsLedger = accountsHead.IsLedger,
          AuditFields = new AuditFields
          {
            InsertedBy = message.CurrentUserName,
            InsertedDateTime = DateTime.Now,
            LastUpdatedBy = message.CurrentUserName,
            LastUpdatedDateTime = DateTime.Now
          },
          BranchMedium = branchMedium,
          Session = sessionMapper.Map(session, new Session())
          
        };
        branchMediumAccountsHeadList.Add(branchMediumAccountHead);
        _dbContext.BranchMediumAccountsHeads.Add(branchMediumAccountHead);
      }

      _dbContext.SaveChanges();
    }
  }
}