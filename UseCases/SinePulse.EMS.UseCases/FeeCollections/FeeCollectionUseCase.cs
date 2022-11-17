using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Accounts;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Students;
using SinePulse.EMS.Domain.Transactions;
using SinePulse.EMS.Messages.FeeCollection;
using SinePulse.EMS.Messages.TransactionMessages;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Persistence.Repositories;
using SinePulse.EMS.UseCases.Transactions;

namespace SinePulse.EMS.UseCases.FeeCollections
{
  public class FeeCollectionUseCase : IFeeCollectionUseCase
  {
    private readonly IRepository _repository;
    private readonly EmsDbContext _dbContext;
    private readonly IAddCashVoucherJournalTransactionUseCase _addCashVoucherJournalTransactionUseCase;
    private readonly IAddBankVoucherJournalTransactionUseCase _addBankVoucherJournalTransactionUseCase;
    private readonly IAddGenericJournalTransactionUseCase _addGenericJournalTransactionUseCase;

    public FeeCollectionUseCase(IRepository repository, EmsDbContext dbContext,
      IAddCashVoucherJournalTransactionUseCase addCashVoucherJournalTransactionUseCase,
      IAddBankVoucherJournalTransactionUseCase addBankVoucherJournalTransactionUseCase,
      IAddGenericJournalTransactionUseCase addGenericJournalTransactionUseCase)
    {
      _repository = repository;
      _dbContext = dbContext;
      _addCashVoucherJournalTransactionUseCase = addCashVoucherJournalTransactionUseCase;
      _addBankVoucherJournalTransactionUseCase = addBankVoucherJournalTransactionUseCase;
      _addGenericJournalTransactionUseCase = addGenericJournalTransactionUseCase;
    }
    
    public void CollectFee(FeeCollectionRequestMessage message)
    {
      var student = _repository.GetById<Student>(message.StudentId);
      var session = _repository.GetById<Session>(message.SessionId);
      var academicFees = _repository.Table<AcademicFee>()
        .Include(nameof(AcademicFee.AccountHead))
        .Include(nameof(Class))
        .Where(a => a.FeePeriod == message.FeeType
                    && a.AccountHead.BranchMedium.Id == message.BranchMediumId
                    && a.AccountHead.Session.Id == message.SessionId
                    && a.Class.Id == message.ClassId)
        .ToList();
      var transactionDescription = GetTransactionDescription(message.FeeType, message.Month.ToString("G"),
        student.FullName, session.SessionName, message.CurrentUserName);
      var bankNo = _repository.GetById<BranchMediumAccountHead>(message.BankAccountAccountHeadId)
        ?.AccountHeadName;
      using (var atomicTransaction = _dbContext.Database.BeginTransaction())
      {
        try
        {
          Transaction transaction;
          if (message.PaymentMethod == PaymentMethod.ByBank)
          {
            transaction = CollectFeeByBank(message, session, student, academicFees, bankNo, transactionDescription);
          }
          else
          {
            transaction = CollectFeeByCash(message, session, student, academicFees, transactionDescription);
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

    private Transaction CollectFeeByBank(FeeCollectionRequestMessage message, Session session, Student student,
      List<AcademicFee> academicFees, string bankNo, string transactionDescription)
    {
      var bankTransactionEntries = new List<AddBankVoucherJournalTransactionRequestMessage.TransactionEntry>();
      var feeCollections = new List<FeeCollection>();
      var index = -1;
      foreach (var academicFee in academicFees)
      {
        var amount = academicFee.Fees;
        var waiverFees = message.Waivers[++index];
        var feeCollection = new FeeCollection
        {
          Amount = amount - waiverFees,
          Fees = amount,
          Waiver = waiverFees,
          Month = message.Month,
          CollectedBy = message.CurrentUserName,
          CollectionDate = DateTime.Now,
          Student = student,
          BankAccountNo = bankNo,
          FeeType = message.FeeType,
          Session = session,
          PaymentMethod = message.PaymentMethod
        };
        feeCollections.Add(feeCollection);
        var bankEntry = new AddBankVoucherJournalTransactionRequestMessage.TransactionEntry
        {
          AccountHeadId = academicFee.AccountHead.Id,
          Amount = amount - waiverFees
        };
        bankTransactionEntries.Add(bankEntry);
      }
          
      var transaction = GetBankTransaction(transactionDescription, bankTransactionEntries, message.BranchMediumId, 
        message.SessionId, message.CurrentUserName, message.BankAccountAccountHeadId);
          
      foreach (var feeCollection in feeCollections)
      {
        feeCollection.Transaction = transaction;
        _repository.Insert(feeCollection);
      }
      return transaction;
    }

    private Transaction CollectFeeByCash(FeeCollectionRequestMessage message, Session session, Student student,
      List<AcademicFee> academicFees, string transactionDescription)
    {
      var cashTransactionEntries = new List<AddCashVoucherJournalTransactionRequestMessage.TransactionEntry>();
      var feeCollections = new List<FeeCollection>();
      var index = -1;
      foreach (var academicFee in academicFees)
      {
        var amount = academicFee.Fees;
        var waiverFees = message.Waivers[++index];
        var feeCollection = new FeeCollection
        {
          Amount = amount - waiverFees,
          Fees = amount,
          Waiver = waiverFees,
          Month = message.Month,
          CollectedBy = message.CurrentUserName,
          CollectionDate = DateTime.Now,
          Student = student,
          FeeType = message.FeeType,
          Session = session,
          PaymentMethod = message.PaymentMethod
        };
        feeCollections.Add(feeCollection);
        var cashEntry = new AddCashVoucherJournalTransactionRequestMessage.TransactionEntry
        {
          AccountHeadId = academicFee.AccountHead.Id,
          Amount = amount - waiverFees
        };
        cashTransactionEntries.Add(cashEntry);
      }
      var transaction = GetCashTransaction(transactionDescription, cashTransactionEntries, message.BranchMediumId, 
        message.SessionId, message.CurrentUserName);
          
      foreach (var feeCollection in feeCollections)
      {
        feeCollection.Transaction = transaction;
        _repository.Insert(feeCollection);
      }
      return transaction; 
    }

    private string GetTransactionDescription(AcademicFeePeriodEnum feeType, string month, string studentName,
      string sessionName, string currentUserName)
    {
      if (feeType == AcademicFeePeriodEnum.Monthly)
        return "Monthly Academic Fee for " + month + " of " + studentName + " for " + sessionName + " has been collected by " +
               currentUserName + ".";
      return "Yearly Academic Fee of " + studentName + " for " + sessionName + " has been collected by " +
             currentUserName + ".";
    }

    private Transaction GetCashTransaction(string description, 
      List<AddCashVoucherJournalTransactionRequestMessage.TransactionEntry> transactionEntries, 
      long branchMediumId, long sessionId, string currentUserName)
    {
      var requestMessage = new AddCashVoucherJournalTransactionRequestMessage
      {
        TransactionDate = DateTime.Now,
        Description = description,
        IsDebitTransaction = true,
        TransactionEntries = transactionEntries,
        BranchMediumId = branchMediumId,
        SessionId = sessionId,
        CurrentUserName = currentUserName
      };
      var addGenericJournalTransactionRequestMessage = _addCashVoucherJournalTransactionUseCase.GetGenericJournalTransactionRequestMessage(requestMessage);
      var transaction = _addGenericJournalTransactionUseCase.AddTransaction(addGenericJournalTransactionRequestMessage);
      return transaction;
    }

    private Transaction GetBankTransaction(string description, 
      List<AddBankVoucherJournalTransactionRequestMessage.TransactionEntry> transactionEntries, 
      long branchMediumId, long sessionId, string currentUserName, long bankAccountHeadId)
    {
      var requestMessage = new AddBankVoucherJournalTransactionRequestMessage
      {
        TransactionDate = DateTime.Now,
        Description = description,
        IsDebitTransaction = true,
        TransactionEntries = transactionEntries,
        BranchMediumId = branchMediumId,
        SessionId = sessionId,
        CurrentUserName = currentUserName,
        BankAccountHeadId = bankAccountHeadId
      };
      var addGenericJournalTransactionRequestMessage = _addBankVoucherJournalTransactionUseCase.GetGenericJournalTransactionRequestMessage(requestMessage);
      var transaction = _addGenericJournalTransactionUseCase.AddTransaction(addGenericJournalTransactionRequestMessage);
      return transaction;
    }
  }
}