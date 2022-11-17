using FluentValidation.Results;
using SinePulse.EMS.Messages.Model.Leaves;
using SinePulse.EMS.Messages.Model.Transactions;
using System.Collections.Generic;

namespace SinePulse.EMS.Messages.TransactionMessages
{
  public class ShowTransactionResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public TransactionMessageModel Transaction { get; set; }
    public ShowTransactionResponseMessage(ValidationResult validationResult, TransactionMessageModel transaction)
    {
      ValidationResult = validationResult;
      Transaction = transaction;
    }
  }
}