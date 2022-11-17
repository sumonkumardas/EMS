using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.Domain.Accounts;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Persistence.Repositories;
using SinePulse.EMS.ProjectPrimitives;

namespace SinePulse.EMS.UseCases.Transactions
{
  public class TransactionEntryDecider : ITransactionEntryValueDecider, ITransactionEntryTypeDecider
  {
    private readonly IRepository _repository;

    public TransactionEntryDecider(IRepository repository)
    {
      _repository = repository;
    }

    public async Task<decimal> GetTransactionEntryValueFromAccountType(decimal amount, long accountTypeId,
      TransactionEntryType transactionEntryType)
    {
      var accountTransactionTypes = await GetAccountTransactionTypes(accountTypeId);
      var accountTransactionTypeEnum = ToAccountTransactionTypeEnum(transactionEntryType);
      var accountTransactionType = accountTransactionTypes.First(x => x.TransactionType == accountTransactionTypeEnum);
      return CorrectAmountSign(amount, accountTransactionType.IncreaseDecreaseType);
    }

    public async Task<TransactionEntryType> GetTransactionEntryTypeFromAccountType(decimal amount, long accountTypeId)
    {
      var accountTransactionTypes = await GetAccountTransactionTypes(accountTypeId);
      var increaseDecreaseEnum = ToIncreaseDecreaseEnum(amount);
      var accountTransactionType = accountTransactionTypes.First(x => x.IncreaseDecreaseType == increaseDecreaseEnum);
      return ToTransactionEntryType(accountTransactionType.TransactionType);
    }

    private async Task<List<AccountTransactionType>> GetAccountTransactionTypes(long accountTypeId)
    {
      return await _repository.Filter<AccountTransactionType>(x => x.AccountType.Id == accountTypeId).ToListAsync();
    }

    private TransactionEntryType ToTransactionEntryType(AccountTransactionTypeEnum accountTransactionTypeEnum)
    {
      switch (accountTransactionTypeEnum)
      {
        case AccountTransactionTypeEnum.Debit:
          return TransactionEntryType.Debit;
        case AccountTransactionTypeEnum.Credit:
          return TransactionEntryType.Credit;
        default:
          throw new ArgumentOutOfRangeException(nameof(accountTransactionTypeEnum), accountTransactionTypeEnum, null);
      }
    }

    private AccountTransactionTypeEnum ToAccountTransactionTypeEnum(TransactionEntryType transactionEntryType)
    {
      switch (transactionEntryType)
      {
        case TransactionEntryType.Debit:
          return AccountTransactionTypeEnum.Debit;
        case TransactionEntryType.Credit:
          return AccountTransactionTypeEnum.Credit;
        default:
          throw new ArgumentOutOfRangeException(nameof(transactionEntryType), transactionEntryType, null);
      }
    }

    private IncreaseDecreaseEnum ToIncreaseDecreaseEnum(decimal amount)
    {
      return amount > 0 ? IncreaseDecreaseEnum.Increase : IncreaseDecreaseEnum.Decrease;
    }

    private decimal CorrectAmountSign(decimal amount, IncreaseDecreaseEnum increaseDecreaseEnum)
    {
      switch (increaseDecreaseEnum)
      {
        case IncreaseDecreaseEnum.Increase:
          return amount;
        case IncreaseDecreaseEnum.Decrease:
          return -amount;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }
  }
}