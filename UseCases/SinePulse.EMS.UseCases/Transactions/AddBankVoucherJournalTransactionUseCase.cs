using System.Collections.Generic;
using SinePulse.EMS.Messages.TransactionMessages;
using static SinePulse.EMS.Messages.TransactionMessages.AddGenericJournalTransactionRequestMessage;

namespace SinePulse.EMS.UseCases.Transactions
{
  public class AddBankVoucherJournalTransactionUseCase : IAddBankVoucherJournalTransactionUseCase
  {
    private readonly IAddGenericJournalTransactionUseCase _addGenericJournalTransactionUseCase;

    public AddBankVoucherJournalTransactionUseCase(
      IAddGenericJournalTransactionUseCase addGenericJournalTransactionUseCase)
    {
      _addGenericJournalTransactionUseCase = addGenericJournalTransactionUseCase;
    }

    public AddBankVoucherJournalTransactionResponseMessage AddBankVoucherJournalTransaction(
      AddBankVoucherJournalTransactionRequestMessage requestMessage)
    {
      var addGenericJournalTransactionRequestMessage = GetGenericJournalTransactionRequestMessage(requestMessage);
      var responseMessage = _addGenericJournalTransactionUseCase.AddGenericJournalTransaction(
        addGenericJournalTransactionRequestMessage);

      return new AddBankVoucherJournalTransactionResponseMessage
      {
        TransactionId = responseMessage.TransactionId,
        TransactionNo = responseMessage.TransactionNo
      };
    }
    public AddGenericJournalTransactionRequestMessage GetGenericJournalTransactionRequestMessage(AddBankVoucherJournalTransactionRequestMessage requestMessage)
    {
      var transactionEntries = new List<TransactionEntry>();
      var totalAmount = 0m;
      foreach (var entry in requestMessage.TransactionEntries)
      {
        var transactionEntry =
          GetTransactionEntry(entry.AccountHeadId, entry.Amount, !requestMessage.IsDebitTransaction);
        totalAmount += entry.Amount;
        transactionEntries.Add(transactionEntry);
      }

      var accountHeadId = GetAccountHeadId(requestMessage);
      var bankTransaction = new TransactionEntry
      {
        AccountHeadId = accountHeadId,
        CreditAmount = requestMessage.IsDebitTransaction ? 0 : totalAmount,
        DebitAmount = requestMessage.IsDebitTransaction ? totalAmount : 0
      };
      transactionEntries.Add(bankTransaction);

      var addGenericJournalTransactionRequestMessage = new AddGenericJournalTransactionRequestMessage
      {
        TransactionEntries = transactionEntries,
        BranchMediumId = requestMessage.BranchMediumId,
        CurrentUserName = requestMessage.CurrentUserName,
        Description = requestMessage.Description,
        SessionId = requestMessage.SessionId,
        TransactionDate = requestMessage.TransactionDate
      };
      return addGenericJournalTransactionRequestMessage;
    }
    private TransactionEntry GetTransactionEntry(long accountHeadId, decimal amount, bool isDebit)
    {
      var transactionEntry = new TransactionEntry
      {
        AccountHeadId = accountHeadId,
        CreditAmount = isDebit ? 0 : amount,
        DebitAmount = isDebit ? amount : 0
      };
      return transactionEntry;
    }

    private long GetAccountHeadId(AddBankVoucherJournalTransactionRequestMessage requestMessage)
    {
      return requestMessage.BankAccountHeadId;
    }
  }
}