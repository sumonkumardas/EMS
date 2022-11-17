using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Accounts;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Domain.Transactions;
using SinePulse.EMS.FinancialJobService.Data;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.FinancialJobService.Services
{
  public class YearCloser : IYearCloser
  {
    private readonly IRepository _repository;
    private readonly EmsDbContext _dbContext;
    private readonly IYearClosingDataProvider _yearClosingDataProvider;
    private readonly IDailyBalanceUpdater _dailyBalanceUpdater;

    public YearCloser(IRepository repository, EmsDbContext dbContext,
      IYearClosingDataProvider yearClosingDataProvider, IDailyBalanceUpdater dailyBalanceUpdater)
    {
      _repository = repository;
      _dbContext = dbContext;
      _yearClosingDataProvider = yearClosingDataProvider;
      _dailyBalanceUpdater = dailyBalanceUpdater;
    }

    public async Task DoYearClosing(long sessionId)
    {
      /*
       * 01. Create New Session
       * 02. Make Year Closing Entry and Corresponding Balance Table Entry for Old Session
       * 03. Copy COA with Academic Fee Setup from Old Session To New Session
       * 04. Make Opening Balance Entry and Corresponding Balance Table Entry for New Session
       * 05. After Year Closing, Old Session's Trial Balance, Income Statement, Balance Sheet will not calculated the Retained Earning
       */
      var oldSession = await _repository.GetByIdAsync<Session, BranchMedium>(sessionId, x => x.BranchMedium);
      var branchMedium = oldSession.BranchMedium;
      var newSession = await InsertNewSession(oldSession, branchMedium);

      var oldAccountHeads = await _repository.Filter<BranchMediumAccountHead, AccountType, BranchMediumAccountHead>(
        x => x.Session.Id == oldSession.Id,
        x => x.AccountType,
        x => x.ParentHead).ToListAsync();

      var retainedEarningsData = await _yearClosingDataProvider.GetRetainedEarningsData(
        oldAccountHeads, oldSession.StartDate, oldSession.EndDate);
      await InsertRetainedEarningsTransaction(oldAccountHeads, oldSession, retainedEarningsData);

      var newAccountHeads =
        await InsertAccountHeadTreeWithAcademicFees(oldAccountHeads, null, branchMedium, newSession);

      var openingBalanceData = await _yearClosingDataProvider.GetOpeningBalanceData(
        oldAccountHeads, oldSession.StartDate, oldSession.EndDate);
      await InsertOpeningBalanceTransaction(newAccountHeads, newSession, openingBalanceData);

      oldSession.IsSessionClosed = true;
    }

    private async Task InsertRetainedEarningsTransaction(IList<BranchMediumAccountHead> accountHeads,
      Session session, RetainedEarningsData retainedEarningsData)
    {
      var transaction = await InsertTransaction($"RE-{session.StartDate.Year}", session.EndDate, "Retained Earning");

      var yearClosingConfiguration = await _repository
        .Filter<AutoPostingConfiguration>(x => x.VoucherType == VoucherTypeEnum.YearClosing).FirstAsync();
      var accountCode = yearClosingConfiguration.AccountCode;
      var retainedEarningsAmount = retainedEarningsData.TotalRevenue - retainedEarningsData.TotalExpense;

      await InsertTransactionEntry(transaction, retainedEarningsAmount, accountHeads, accountCode);
    }

    private async Task InsertOpeningBalanceTransaction(IList<BranchMediumAccountHead> accountHeads,
      Session session, OpeningBalanceData openingBalanceData)
    {
      var transaction = await InsertTransaction($"OB-{session.StartDate.Year}", session.StartDate, "Opening Balance");

      await InsertTransactionEntries(transaction, accountHeads, openingBalanceData.AssetAccountCodeBalances);
      await InsertTransactionEntries(transaction, accountHeads, openingBalanceData.LiabilityAccountCodeBalances);
      await InsertTransactionEntries(transaction, accountHeads, openingBalanceData.EquityAccountCodeBalances);
    }

    private async Task<Transaction> InsertTransaction(string transactionNo, DateTime transactionDate,
      string description)
    {
      var transaction = new Transaction
        {
          TransactionNo = transactionNo,
          TransactionDate = transactionDate,
          Description = description,
          AuditFields = new AuditFields
          {
            InsertedBy = "BatchJob",
            InsertedDateTime = DateTime.Now,
            LastUpdatedBy = "BatchJob",
            LastUpdatedDateTime = DateTime.Now
          }
        }
        ;
      await _repository.InsertAsync(transaction);

      return transaction;
    }

    private async Task InsertTransactionEntries(Transaction transaction, IList<BranchMediumAccountHead> accountHeads,
      IEnumerable<OpeningBalanceData.AccountCodeBalance> accountCodeBalances)
    {
      foreach (var accountHeadBalance in accountCodeBalances)
        await InsertTransactionEntry(transaction, accountHeadBalance.Balance, accountHeads,
          accountHeadBalance.AccountCode);
    }

    private async Task InsertTransactionEntry(Transaction transaction, decimal amount,
      IEnumerable<BranchMediumAccountHead> accountHeads, string accountCode)
    {
      var accountHead = accountHeads.First(x => x.AccountCode == accountCode);
      await InsertTransactionEntry(transaction, amount, accountHead);
    }

    private async Task<TransactionEntry> InsertTransactionEntry(Transaction transaction, decimal amount,
      BranchMediumAccountHead accountHead)
    {
      var transactionEntry = new TransactionEntry
      {
        Amount = amount,
        AuditFields = new AuditFields
        {
          InsertedBy = "BatchJob",
          InsertedDateTime = DateTime.Now,
          LastUpdatedBy = "BatchJob",
          LastUpdatedDateTime = DateTime.Now
        },
        AccountHead = accountHead,
        Transaction = transaction
      };

      await _repository.InsertAsync(transactionEntry);
      await _dbContext.SaveChangesAsync();
      await _dailyBalanceUpdater.UpdateDailyBalance(transaction.TransactionDate, transactionEntry.AccountHead.Id);

      return transactionEntry;
    }

    private async Task<Session> InsertNewSession(Session oldSession, BranchMedium branchMedium)
    {
      var newSessionStartDate = oldSession.EndDate.AddDays(1);
      var newSessionsEndDate = oldSession.StartDate.AddYears(1);
      var sessionName = $"Session-{newSessionStartDate.Year}";

      var newSession = new Session
      {
        SessionName = sessionName,
        StartDate = newSessionStartDate,
        EndDate = newSessionsEndDate,
        IsSessionClosing = false,
        IsSessionClosed = false,
        SessionType = ObjectTypeEnum.BranchMedium,
        Status = oldSession.Status,
        AuditFields = new AuditFields
        {
          InsertedBy = "BatchJob",
          InsertedDateTime = DateTime.Now,
          LastUpdatedBy = "BatchJob",
          LastUpdatedDateTime = DateTime.Now
        },
        BranchMedium = branchMedium
      };
      await _repository.InsertAsync(newSession);
      return newSession;
    }

    private async Task<IList<BranchMediumAccountHead>> InsertAccountHeadTreeWithAcademicFees(
      ICollection<BranchMediumAccountHead> accountHeads,
      BranchMediumAccountHead parentHead, BranchMedium branchMedium, Session newSession)
    {
      var newAccountHeads = new List<BranchMediumAccountHead>();
      foreach (var oldAccountHead in accountHeads.Where(x => x.ParentHead == parentHead))
      {
        var newAccountHead = await InsertNewAccountHead(oldAccountHead, oldAccountHead.AccountType,
          parentHead, branchMedium, newSession);
        var oldAcademicFees = await _repository.Filter<AcademicFee, Class>(
          x => x.AccountHead.Id == oldAccountHead.Id,
          x => x.Class).ToListAsync();
        foreach (var oldAcademicFee in oldAcademicFees)
        {
          await InsertNewAcademicFee(oldAcademicFee, oldAcademicFee.Class, newAccountHead);
        }

        newAccountHeads.Add(newAccountHead);

        var subAccountHeads =
          await InsertAccountHeadTreeWithAcademicFees(accountHeads, newAccountHead, branchMedium, newSession);
        newAccountHeads.AddRange(subAccountHeads);
      }

      return newAccountHeads;
    }

    private async Task<BranchMediumAccountHead> InsertNewAccountHead(BranchMediumAccountHead oldAccountHead,
      AccountType accountType, BranchMediumAccountHead parentHead, BranchMedium branchMedium, Session session)
    {
      var newAccountHead = new BranchMediumAccountHead
      {
        AccountCode = oldAccountHead.AccountCode,
        AccountHeadName = oldAccountHead.AccountHeadName,
        AccountHeadType = oldAccountHead.AccountHeadType,
        AccountPeriod = oldAccountHead.AccountPeriod,
        Status = oldAccountHead.Status,
        IsLedger = oldAccountHead.IsLedger,
        AuditFields = new AuditFields
        {
          InsertedBy = "BatchJob",
          InsertedDateTime = DateTime.Now,
          LastUpdatedBy = "BatchJob",
          LastUpdatedDateTime = DateTime.Now
        },
        AccountType = accountType,
        ParentHead = parentHead,
        BranchMedium = branchMedium,
        Session = session
      };
      await _repository.InsertAsync(newAccountHead);
      return newAccountHead;
    }

    private async Task<AcademicFee> InsertNewAcademicFee(AcademicFee oldAcademicFee,
      Class @class, BranchMediumAccountHead accountHead)
    {
      var newAcademicFee = new AcademicFee
      {
        Fees = oldAcademicFee.Fees,
        AuditFields = new AuditFields
        {
          InsertedBy = "BatchJob",
          InsertedDateTime = DateTime.Now,
          LastUpdatedBy = "BatchJob",
          LastUpdatedDateTime = DateTime.Now
        },
        Class = @class,
        AccountHead = accountHead
      };
      await _repository.InsertAsync(newAcademicFee);
      return newAcademicFee;
    }
  }
}