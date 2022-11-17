using System;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Accounts;
using SinePulse.EMS.Domain.Banks;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.BankAccountMessages;
using SinePulse.EMS.Messages.Model.Banks;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Persistence.Repositories;
using SinePulse.EMS.UseCases.Sessions;

namespace SinePulse.EMS.UseCases.BankAccounts
{
  public class AddBankAccountUseCase : IAddBankAccountUseCase
  {
    private readonly IRepository _repository;
    private readonly EmsDbContext _dbContext;
    private readonly MapperConfiguration _mapperConfiguration;
    private readonly ICurrentSessionProvider _currentSessionProvider;

    public AddBankAccountUseCase(IRepository repository, EmsDbContext dbContext, ICurrentSessionProvider currentSessionProvider)
    {
      _repository = repository;
      _dbContext = dbContext;
      _currentSessionProvider = currentSessionProvider;
      _mapperConfiguration = new MapperConfiguration(cfg => cfg.CreateMap<BankAccount, BankAccountMessageModel>());
    }

    public BankAccountMessageModel AddBankAccount(AddBankAccountRequestMessage message)
    {
      var bankBranch = _repository.GetByIdWithInclude<BankBranch>(message.BankBranchId, 
        new []{nameof(Bank) +"."+ nameof(BranchMedium)});

      var bankAccount = new BankAccount
      {
        AccountNumber = message.AccountNumber,
        AccountType = message.AccountType,
        AuditFields = new AuditFields
        {
          InsertedBy = message.CurrentUserName,
          InsertedDateTime = DateTime.Now,
          LastUpdatedBy = message.CurrentUserName,
          LastUpdatedDateTime = DateTime.Now
        },
        BankBranch = bankBranch
      };

      var bankAccountCode = _repository.Table<AutoPostingConfiguration>()
        .FirstOrDefault(a => a.VoucherType == VoucherTypeEnum.BankVoucher)?.AccountCode;
      var currentSession = _currentSessionProvider.GetCurrentSession(bankBranch.Bank.BranchMedium.Id);
      var bankParentAccountHead = _repository.Table<BranchMediumAccountHead>()
        .Include(nameof(BranchMediumAccountHead.ParentHead))
        .Include(nameof(BranchMediumAccountHead.AccountType))
        .FirstOrDefault(b => b.AccountCode == bankAccountCode
                             && b.Session.Id == currentSession.Id);

      var maxNumber = _repository.Table<BranchMediumAccountHead>()
                        .Include(nameof(BranchMediumAccountHead.ParentHead))
                        .Where(w => w.ParentHead.Id == bankParentAccountHead.Id)
                        .OrderBy(o => o.AccountCode).ToList().Count + 1;


      var parentHead = _repository.Table<BranchMediumAccountHead>()
        .Include(nameof(BranchMediumAccountHead.ParentHead))
        .Include(nameof(BranchMediumAccountHead.AccountType))
        .Include(nameof(Session))
        .Include(nameof(BranchMedium))
        .FirstOrDefault(x => x.AccountCode == bankParentAccountHead.AccountCode
                             && x.AccountHeadName == bankParentAccountHead.AccountHeadName
                             && x.AccountType.Id == bankParentAccountHead.AccountType.Id);

      var branchMediumAccountsHead = new BranchMediumAccountHead
      {
        AccountCode = bankParentAccountHead.AccountCode.Substring(0, 3) + maxNumber,
        AccountHeadName = bankAccount.AccountNumber,
        ParentHead = parentHead,
        AccountHeadType = AccountHeadTypeEnum.Bank,
        AccountType = bankParentAccountHead.AccountType,
        AccountPeriod = AccountPeriodEnum.None,
        IsLedger = true,
        Status = StatusEnum.Active,
        AuditFields = new AuditFields
        {
          InsertedBy = message.CurrentUserName,
          InsertedDateTime = DateTime.Now,
          LastUpdatedBy = message.CurrentUserName,
          LastUpdatedDateTime = DateTime.Now
        },
        BranchMedium = parentHead.BranchMedium,
        Session = parentHead.Session,
      };

      _repository.Insert(bankAccount);
      _repository.Insert(branchMediumAccountsHead);
      _dbContext.SaveChanges();
      var mapper = _mapperConfiguration.CreateMapper();
      return mapper.Map(bankAccount, new BankAccountMessageModel());
    }
  }
}