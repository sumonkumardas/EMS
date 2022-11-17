using System;
using FluentValidation.Results;
using System.Collections.Generic;

namespace SinePulse.EMS.Messages.TransactionMessages
{
  public class ShowTransactionListResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public List<Transaction> TransactionList { get; set; }
    public ShowTransactionListResponseMessage(ValidationResult validationResult, List<Transaction> transactionList)
    {
      ValidationResult = validationResult;
      TransactionList = transactionList;
    }

    public class Transaction
    {
      public long TransactionId { get; set; }
      public string TransactionNo { get; set; }
      public DateTime TransactionDate { get; set; }
      public string Description { get; set; }

    }
  }
}