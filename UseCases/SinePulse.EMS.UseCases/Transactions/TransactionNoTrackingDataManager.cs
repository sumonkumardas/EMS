using System;
using System.Linq;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Domain.Transactions;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Transactions
{
  public class TransactionNoTrackingDataManager : ITransactionNoTrackingDataManager
  {
    private readonly IRepository _repository;

    public TransactionNoTrackingDataManager(IRepository repository)
    {
      _repository = repository;
    }

    public string GetNextTransactionNo(string transactionPrefix, long branchMediumId, string currentUserName)
    {
      var transactionNoTrackingData = _repository.Filter<TransactionNoTrackingData>(
        x => x.TransactionPrefix == transactionPrefix && x.BranchMediumId == branchMediumId).FirstOrDefault();
      var nextTransactionNoCounter = GetNextTransactionNoCounter(transactionNoTrackingData);
      if (transactionNoTrackingData == null)
      {
        InsertTransactionNoTrackingData(transactionPrefix, branchMediumId, currentUserName);
      }
      else
      {
        UpdateTransactionNoTrackingData(transactionNoTrackingData, currentUserName);
      }

      return $"{transactionPrefix}{nextTransactionNoCounter}";
    }

    private void InsertTransactionNoTrackingData(string transactionPrefix, long branchMediumId, string currentUserName)
    {
      _repository.Insert(new TransactionNoTrackingData
      {
        TransactionPrefix = transactionPrefix,
        BranchMediumId = branchMediumId,
        NextTransactionNoCounter = 2,
        AuditFields = new AuditFields
        {
          InsertedBy = currentUserName,
          InsertedDateTime = DateTime.Now,
          LastUpdatedBy = currentUserName,
          LastUpdatedDateTime = DateTime.Now
        }
      });
    }

    private static void UpdateTransactionNoTrackingData(TransactionNoTrackingData transactionNoTrackingData,
      string currentUserName)
    {
      transactionNoTrackingData.NextTransactionNoCounter += 1;
      transactionNoTrackingData.AuditFields.LastUpdatedBy = currentUserName;
      transactionNoTrackingData.AuditFields.LastUpdatedDateTime = DateTime.Now;
    }

    private static int GetNextTransactionNoCounter(TransactionNoTrackingData transactionNoTrackingData)
    {
      return transactionNoTrackingData?.NextTransactionNoCounter ?? 1;
    }
  }
}