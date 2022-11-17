using System;
using AutoMapper;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Banks;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.BankMessages;
using SinePulse.EMS.Messages.Model.Banks;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Banks
{
  public class AddBankUseCase : IAddBankUseCase
  {
    private readonly IRepository _repository;
    private readonly EmsDbContext _dbContext;
    private readonly MapperConfiguration _mapperConfiguration;

    public AddBankUseCase(IRepository repository, EmsDbContext dbContext)
    {
      _repository = repository;
      _dbContext = dbContext;
      _mapperConfiguration = new MapperConfiguration(c => c.CreateMap<Bank, BankMessageModel>());
    }

    public BankMessageModel AddBank(AddBankRequestMessage message)
    {
      var branchMedium = _repository.GetById<BranchMedium>(message.BranchMediumId);
      var bank = new Bank
      {
        BankName = message.BankName,
        AuditFields = new AuditFields
        {
          InsertedBy = message.CurrentUserName,
          InsertedDateTime = DateTime.Now,
          LastUpdatedBy = message.CurrentUserName,
          LastUpdatedDateTime = DateTime.Now
        },
        BranchMedium = branchMedium
      };
      _repository.Insert(bank);
      _dbContext.SaveChanges();
      var mapper = _mapperConfiguration.CreateMapper();
      return mapper.Map(bank, new BankMessageModel());
    }
  }
}