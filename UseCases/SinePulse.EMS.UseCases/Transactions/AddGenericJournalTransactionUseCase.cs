using System;
using System.Linq;
using System.Threading.Tasks;
using SinePulse.EMS.Constants;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Accounts;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Domain.Transactions;
using SinePulse.EMS.Messages.FinancialJobMessages;
using SinePulse.EMS.Messages.TransactionMessages;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Persistence.Repositories;
using SinePulse.EMS.Utility.MessagingQueue;

namespace SinePulse.EMS.UseCases.Transactions
{
  public class AddGenericJournalTransactionUseCase : IAddGenericJournalTransactionUseCase
  {
    private readonly IRepository _repository;
    private readonly EmsDbContext _dbContext;
    private readonly ITransactionNoTrackingDataManager _transactionNoTrackingDataManager;
    private readonly IMessageSender _messageSender;

    public AddGenericJournalTransactionUseCase(IRepository repository, EmsDbContext dbContext,
      ITransactionNoTrackingDataManager transactionNoTrackingDataManager,
      IMessageSender messageSender)
    {
      _repository = repository;
      _dbContext = dbContext;
      _transactionNoTrackingDataManager = transactionNoTrackingDataManager;
      _messageSender = messageSender;
    }

    public AddGenericJournalTransactionResponseMessage AddGenericJournalTransaction(
      AddGenericJournalTransactionRequestMessage requestMessage)
    {
      using (var atomicTransaction = _dbContext.Database.BeginTransaction())
      {
        try
        {
          var transaction = AddTransaction(requestMessage);
          _dbContext.SaveChanges();
          atomicTransaction.Commit();

          SendBalanceChangedNotificationMessages(transaction).GetAwaiter().GetResult();

          return new AddGenericJournalTransactionResponseMessage
          {
            TransactionId = transaction.Id,
            TransactionNo = transaction.TransactionNo
          };
        }
        catch (Exception)
        {
          atomicTransaction.Rollback();
          throw;
        }
      }
    }

    public Transaction AddTransaction(AddGenericJournalTransactionRequestMessage requestMessage)
    {
      var transactionNo = GetTransactionNo(requestMessage);
      var transaction = new Transaction
      {
        TransactionNo = transactionNo,
        Description = requestMessage.Description,
        TransactionDate = requestMessage.TransactionDate,
        AuditFields = new AuditFields
        {
          InsertedBy = requestMessage.CurrentUserName,
          InsertedDateTime = DateTime.Now,
          LastUpdatedBy = requestMessage.CurrentUserName,
          LastUpdatedDateTime = DateTime.Now
        }
      };
      _repository.Insert(transaction);
      foreach (var entry in requestMessage.TransactionEntries)
      {
        var accountsHead = _repository.GetById<BranchMediumAccountHead, AccountType>(
          entry.AccountHeadId,
          x => x.AccountType);
        var transactionType = GetTransactionType(entry);
        var accountTransactionType = _repository
          .Filter<AccountTransactionType>(x => x.AccountType.Id == accountsHead.AccountType.Id).AsEnumerable()
          .First(x => x.TransactionType == transactionType);
        var amount = GetAmount(accountTransactionType, entry);
        var transactionEntry = new TransactionEntry
        {
          Amount = amount,
          AuditFields = new AuditFields
          {
            InsertedBy = requestMessage.CurrentUserName,
            InsertedDateTime = DateTime.Now,
            LastUpdatedBy = requestMessage.CurrentUserName,
            LastUpdatedDateTime = DateTime.Now
          },
          Transaction = transaction,
          AccountHead = accountsHead
        };

        _repository.Insert(transactionEntry);
      }

      return transaction;
    }

    private string GetTransactionNo(AddGenericJournalTransactionRequestMessage requestMessage)
    {
      var accountHeadId = requestMessage.TransactionEntries.First().AccountHeadId;
      var accountsHead = _repository.GetById<BranchMediumAccountHead, BranchMedium>(accountHeadId,
        x => x.BranchMedium);
      return _transactionNoTrackingDataManager.GetNextTransactionNo("CS-", accountsHead.BranchMedium.Id,
        requestMessage.CurrentUserName);
    }

    private static decimal GetAmount(AccountTransactionType accountTransactionType,
      AddGenericJournalTransactionRequestMessage.TransactionEntry transactionEntry)
    {
      var amount = accountTransactionType.TransactionType == AccountTransactionTypeEnum.Debit
        ? transactionEntry.DebitAmount
        : transactionEntry.CreditAmount;
      return accountTransactionType.IncreaseDecreaseType == IncreaseDecreaseEnum.Increase ? amount : -amount;
    }

    private static AccountTransactionTypeEnum GetTransactionType(
      AddGenericJournalTransactionRequestMessage.TransactionEntry transactionEntry)
    {
      return transactionEntry.DebitAmount != 0 ? AccountTransactionTypeEnum.Debit : AccountTransactionTypeEnum.Credit;
    }

    public async Task SendBalanceChangedNotificationMessages(Transaction transaction)
    {
      foreach (var transactionEntry in transaction.TransactionEntries)
      {
        var message = new BalanceChangedNotificationMessage
        {
          ChangedAmount = transactionEntry.Amount,
          Date = transaction.TransactionDate,
          BranchMediumAccountsHeadId = transactionEntry.AccountHead.Id
        };
        await _messageSender.Send(message, MicroServiceAddresses.FinancialJobService);
      }
    }
  }
}