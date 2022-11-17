using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Banks;
using SinePulse.EMS.Messages.BankBranchMessages;
using SinePulse.EMS.Messages.Model.Banks;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.BankBranches
{
  public class ShowBankBranchUseCase : IShowBankBranchUseCase
  {
    private readonly IRepository _repository;
    private readonly MapperConfiguration _mapperConfiguration;

    public ShowBankBranchUseCase(IRepository repository)
    {
      _repository = repository;
      _mapperConfiguration = new MapperConfiguration(c => c.CreateMap<BankBranch, BankBranchMessageModel>());
    }

    public BankBranchMessageModel GetBankBranch(ShowBankBranchRequestMessage message)
    {
      var bankBranch = _repository.Table<BankBranch>()
        .Include(nameof(Bank) + "." + nameof(BranchMedium) + "." + nameof(BranchMedium.Branch) +"."+ nameof(Institute))
        .Include(nameof(Bank) + "." + nameof(BranchMedium) + "." + nameof(BranchMedium.Medium))
        .Include(nameof(Bank) + "." + nameof(BranchMedium) + "." + nameof(BranchMedium.Shift))
        .Include(nameof(BankBranch.BankAccounts))
        .FirstOrDefault(b => b.Id == message.BankBranchId);
      var mapper = _mapperConfiguration.CreateMapper();
      return mapper.Map(bankBranch, new BankBranchMessageModel());
    }
  }
}