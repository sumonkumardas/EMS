using System;
using System.Collections.Generic;
using System.Linq;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Accounts;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Payroll;
using SinePulse.EMS.Domain.Transactions;
using SinePulse.EMS.Messages.SalarySheetMessages;
using SinePulse.EMS.Messages.TransactionMessages;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Persistence.Repositories;
using SinePulse.EMS.UseCases.Sessions;
using SinePulse.EMS.UseCases.Transactions;

namespace SinePulse.EMS.UseCases.SalarySheets
{
  public class SalarySheetAccountPostingUseCase : ISalarySheetAccountPostingUseCase
  {
    private readonly IRepository _repository;
    private readonly EmsDbContext _dbContext;
    private readonly IAddGenericJournalTransactionUseCase _addGenericJournalTransactionUseCase;
    private readonly ICurrentSessionProvider _currentSessionProvider;

    public SalarySheetAccountPostingUseCase(IRepository repository, EmsDbContext dbContext,
      IAddGenericJournalTransactionUseCase addGenericJournalTransactionUseCase,
      ICurrentSessionProvider currentSessionProvider)
    {
      _repository = repository;
      _dbContext = dbContext;
      _addGenericJournalTransactionUseCase = addGenericJournalTransactionUseCase;
      _currentSessionProvider = currentSessionProvider;
    }

    public SalarySheetAccountPostingResponseMessage.AccountPostResponse PostSalarySheetAccount(
      SalarySheetAccountPostingRequestMessage message)
    {
      var salarySheet = _repository.Table<SalarySheet>()
        .FirstOrDefault(s => s.Year == message.Year &&
                             s.Month == message.Month &&
                             s.BranchMedium.Id == message.BranchMediumId);
      var accountPostResponse = new SalarySheetAccountPostingResponseMessage.AccountPostResponse();
      if (salarySheet != null && salarySheet.IsAccountPosted)
      {
        accountPostResponse.AccountAlreadyPosted = true;
        return accountPostResponse;
      }
      var currentSession = _currentSessionProvider.GetCurrentSession(message.BranchMediumId);
      var wagesAccountCode = GetAccountCode(VoucherTypeEnum.WagesExpense);
      var wagesPayableAccountCode = GetAccountCode(VoucherTypeEnum.WagesPayable);
      var wagesAccountHead = GetAccountHead(wagesAccountCode, message.BranchMediumId, currentSession);
      var wagesPayableAccountHead = GetAccountHead(wagesPayableAccountCode, message.BranchMediumId, currentSession);
      var taxExpenseAccountCode = GetAccountCode(VoucherTypeEnum.PayrollTaxExpense);
      var taxPayableAccountCode = GetAccountCode(VoucherTypeEnum.PayrollTaxPayable);
      var taxExpenseAccountHead = GetAccountHead(taxExpenseAccountCode, message.BranchMediumId, currentSession);
      var taxPayableAccountHead = GetAccountHead(taxPayableAccountCode, message.BranchMediumId, currentSession);
      
      if (wagesAccountHead != null && 
          wagesPayableAccountHead != null && 
          taxExpenseAccountHead != null &&
          taxPayableAccountHead != null &&
          salarySheet != null && 
          !salarySheet.IsAccountPosted)
      {
        using (var atomicTransaction = _dbContext.Database.BeginTransaction())
        {
          try
          {
            var transactionEntries = GetTransactionEntries(salarySheet, wagesAccountHead, wagesPayableAccountHead, 
              taxExpenseAccountHead, taxPayableAccountHead);
            var addGenericJournalTransaction = new AddGenericJournalTransactionRequestMessage
            {
              TransactionDate = DateTime.Now,
              Description = GetDescription(currentSession, message.Month),
              TransactionEntries = transactionEntries,
              BranchMediumId = message.BranchMediumId,
              SessionId = currentSession.Id,
              CurrentUserName = message.CurrentUserName,
            };

            var transaction = _addGenericJournalTransactionUseCase.AddTransaction(addGenericJournalTransaction);
            if (transaction != null)
            {
              UpdateSalarySheetTable(message.Year, message.Month, message.BranchMediumId, transaction);
              accountPostResponse.AccountPostingSuccessful = true;
            }
            else
            {
              accountPostResponse.AccountPostingFailed = true;
            }
            _dbContext.SaveChanges();
            atomicTransaction.Commit();
            _addGenericJournalTransactionUseCase.SendBalanceChangedNotificationMessages(transaction);
          }
          catch (Exception)
          {
            atomicTransaction.Rollback();
            throw;
          }
        }
      }

      var isChartOfAccountsImported = _repository.Table<BranchMediumAccountHead>()
        .Any(b => currentSession != null && 
                  b.BranchMedium.Id == message.BranchMediumId && 
                  b.Session.Id == currentSession.Id);
      if (!isChartOfAccountsImported)
        accountPostResponse.ImportChartOfAccounts = true;
      return accountPostResponse;
    }

    private BranchMediumAccountHead GetAccountHead(string accountCode, long branchMediumId, Session currentSession)
    {
      return _repository.Table<BranchMediumAccountHead>()
        .FirstOrDefault(b => currentSession != null &&
                             accountCode != null &&
                             b.AccountCode == accountCode &&
                             b.BranchMedium.Id == branchMediumId &&
                             b.Session.Id == currentSession.Id);
    }

    private string GetAccountCode(VoucherTypeEnum voucherType)
    {
      return _repository.Table<AutoPostingConfiguration>()
        .FirstOrDefault(a => a.VoucherType == voucherType)
        ?.AccountCode;
    }

    private List<AddGenericJournalTransactionRequestMessage.TransactionEntry> GetTransactionEntries(
      SalarySheet salarySheet, BranchMediumAccountHead wagesAccountHead,
      BranchMediumAccountHead wagesPayableAccountHead, BranchMediumAccountHead taxExpenseAccountHead,
      BranchMediumAccountHead taxPayableAccountHead)
    {
      var transactionEntries = new List<AddGenericJournalTransactionRequestMessage.TransactionEntry>();
      var wagesExpense = new AddGenericJournalTransactionRequestMessage.TransactionEntry
      {
        AccountHeadId = wagesAccountHead.Id,
        DebitAmount = salarySheet.TotalSalary
      };
      transactionEntries.Add(wagesExpense);
      var wagesPayable = new AddGenericJournalTransactionRequestMessage.TransactionEntry
      {
        AccountHeadId = wagesPayableAccountHead.Id,
        CreditAmount = salarySheet.TotalSalary
      };
      transactionEntries.Add(wagesPayable);
      var taxExpense = new AddGenericJournalTransactionRequestMessage.TransactionEntry
      {
        AccountHeadId = taxExpenseAccountHead.Id,
        DebitAmount = salarySheet.TotalTaxes
      };
      transactionEntries.Add(taxExpense);
      var taxPayable = new AddGenericJournalTransactionRequestMessage.TransactionEntry
      {
        AccountHeadId = taxPayableAccountHead.Id,
        CreditAmount = salarySheet.TotalTaxes
      };
      transactionEntries.Add(taxPayable);
      return transactionEntries;
    }

    private void UpdateSalarySheetTable(int year, int month, long branchMediumId, Transaction transaction)
    {
      var salarySheet = _repository.Table<SalarySheet>()
        .FirstOrDefault(s => s.Year == year &&
                             s.Month == month &&
                             s.BranchMedium.Id == branchMediumId);
      if (salarySheet != null)
      {
        salarySheet.IsAccountPosted = true;
        salarySheet.Transaction = transaction;
      }
    }

    private string GetDescription(Session currentSession, int month)
    {
      return "Salary of Employees of " + ((MonthsOfYearEnum) month).ToString("G") + " , " + currentSession.SessionName;
    }
  }
}