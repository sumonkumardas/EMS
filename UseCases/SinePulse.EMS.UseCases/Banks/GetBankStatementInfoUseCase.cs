using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Banks;
using SinePulse.EMS.Messages.BankMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Banks
{
  public class GetBankStatementInfoUseCase : IGetBankStatementInfoUseCase
  {
    private readonly IRepository _repository;

    public GetBankStatementInfoUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public GetBankStatementInfoResponseMessage.BankStatementInfo GetBankStatementInfo(
      GetBankStatementInfoRequestMessage message)
    {
      var bankStatementInfos = new GetBankStatementInfoResponseMessage.BankStatementInfo();
      var branchMedium = _repository.Table<BranchMedium>()
        .Include(nameof(Branch) + "." + nameof(Institute))
        .FirstOrDefault(b => b.Id == message.BranchMediumId);
      var bankAccount = _repository.Table<BankAccount>()
        .Include(nameof(BankBranch) + "." + nameof(Bank))
        .FirstOrDefault(b => b.AccountNumber == message.AccountNumber);
      if (bankAccount != null)
      {
        bankStatementInfos.BankName = bankAccount.BankBranch.Bank.BankName;
        bankStatementInfos.BankBranchName = bankAccount.BankBranch.BranchName;
        bankStatementInfos.BankBranchAddress = bankAccount.BankBranch.Address;
      }

      if (branchMedium != null)
      {
        bankStatementInfos.InstituteName = branchMedium.Branch.Institute.OrganisationName;
        bankStatementInfos.BranchName = branchMedium.Branch.BranchName;
      }

      return bankStatementInfos;
    }
  }
}