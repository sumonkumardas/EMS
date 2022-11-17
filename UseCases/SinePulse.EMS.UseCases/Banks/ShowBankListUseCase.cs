using System.Collections.Generic;
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
  public class ShowBankListUseCase : IShowBankListUseCase
  {
    private readonly IRepository _repository;

    public ShowBankListUseCase(IRepository repository)
    {
      _repository = repository;
    }



    public List<ShowBankListResponseMessage.Bank> GetBankList(ShowBankListRequestMessage message)
    {
      var bankList = _repository.Table<Bank>()
        .Include(nameof(BranchMedium) + "." + nameof(BranchMedium.Branch) + "." + nameof(Institute))
        .Include(nameof(BranchMedium) + "." + nameof(BranchMedium.Medium))
        .Include(nameof(Bank.BankBranches) + "." + nameof(BankBranch.BankAccounts))
        .Where(x => x.BranchMedium.Id == message.BranchMediumId);

      var bankInfoList = new List<ShowBankListResponseMessage.Bank>();

      var bankInfo = new ShowBankListResponseMessage.Bank();
      
      foreach (var bank in bankList)
      {
        bankInfo.BankId = bank.Id;
        bankInfo.BankName = bank.BankName;
        if (!bank.BankBranches.Any())
        {
          bankInfoList.Add(bankInfo);
          bankInfo = new ShowBankListResponseMessage.Bank();
        }

        foreach (var bankBankBranch in bank.BankBranches)
        {
          bankInfo.BankBranchName = bankBankBranch.BranchName;
          bankInfo.BankBranchId = bankBankBranch.Id;
          if (!bankBankBranch.BankAccounts.Any())
          {
            bankInfo.BankId = bank.Id;
            bankInfo.BankName = bank.BankName;
            bankInfoList.Add(bankInfo);
            bankInfo = new ShowBankListResponseMessage.Bank();
          }

          foreach (var bankAccount in bankBankBranch.BankAccounts)
          {
            bankInfo.BankId = bank.Id;
            bankInfo.BankName = bank.BankName;
            bankInfo.BankBranchName = bankBankBranch.BranchName;
            bankInfo.BankBranchId = bankBankBranch.Id;
            bankInfo.AccountNumber = bankAccount.AccountNumber;
            bankInfoList.Add(bankInfo);
            bankInfo = new ShowBankListResponseMessage.Bank();
          }
        }
        
      }

      return bankInfoList;
    }
  }
}