using System;
using AutoMapper;
using SinePulse.EMS.Domain.Banks;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.BankBranchMessages;
using SinePulse.EMS.Messages.Model.Banks;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Persistence.Repositories;
using SinePulse.EMS.UseCases.Banks;

namespace SinePulse.EMS.UseCases.BankBranches
{
  public class AddBankBranchUseCase : IAddBankBranchUseCase
  {
    private readonly IRepository _repository;
    private readonly EmsDbContext _dbContext;
    private readonly MapperConfiguration _mapperConfiguration;

    public AddBankBranchUseCase(IRepository repository, EmsDbContext dbContext)
    {
      _repository = repository;
      _dbContext = dbContext;
      _mapperConfiguration = new MapperConfiguration(c => c.CreateMap<BankBranch, BankBranchMessageModel>());
    }

    public BankBranchMessageModel AddBankBranch(AddBankBranchRequestMessage message)
    {
      var bank = _repository.GetById<Bank>(message.BankId);
      var bankBranch = new BankBranch
      {
        Address = message.Address,
        BranchName = message.BranchName,
        AuditFields = new AuditFields
        {
          InsertedBy = message.CurrentUserName,
          InsertedDateTime = DateTime.Now,
          LastUpdatedBy = message.CurrentUserName,
          LastUpdatedDateTime = DateTime.Now
        },
        Bank = bank
      };
      
      _repository.Insert(bankBranch);
      _dbContext.SaveChanges();
      var mapper = _mapperConfiguration.CreateMapper();
      return mapper.Map(bankBranch, new BankBranchMessageModel());
    }
  }
}