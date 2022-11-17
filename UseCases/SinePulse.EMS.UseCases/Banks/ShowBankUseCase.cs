using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Banks;
using SinePulse.EMS.Messages.BankMessages;
using SinePulse.EMS.Messages.Model.Banks;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Banks
{
  public class ShowBankUseCase : IShowBankUseCase
  {
    private readonly IRepository _repository;
    private readonly MapperConfiguration _mapperConfiguration;

    public ShowBankUseCase(IRepository repository)
    {
      _repository = repository;
      _mapperConfiguration = new MapperConfiguration(c => c.CreateMap<Bank, BankMessageModel>());
    }

    public BankMessageModel GetBank(ShowBankRequestMessage message)
    {
      var mapper = _mapperConfiguration.CreateMapper();
      var bank = _repository.Table<Bank>()
        .Include(nameof(BranchMedium) +"."+ nameof(BranchMedium.Branch) +"."+ nameof(Institute))
        .Include(nameof(BranchMedium) +"."+ nameof(BranchMedium.Medium))
        .Include(nameof(BranchMedium) + "." + nameof(BranchMedium.Shift))
        .Include(nameof(Bank.BankBranches) +"."+ nameof(BankBranch.BankAccounts))
        .FirstOrDefault(x => x.Id == message.BankId);

      return mapper.Map(bank, new BankMessageModel());
    }
  }
}