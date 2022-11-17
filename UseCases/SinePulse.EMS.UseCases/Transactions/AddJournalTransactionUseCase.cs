using System.Collections.Generic;
using SinePulse.EMS.Messages.TransactionMessages;
using static SinePulse.EMS.Messages.TransactionMessages.AddGenericJournalTransactionRequestMessage;

namespace SinePulse.EMS.UseCases.Transactions
{
  public class AddJournalTransactionUseCase : IAddJournalTransactionUseCase
  {
    private readonly IAddGenericJournalTransactionUseCase _addGenericJournalTransactionUseCase;

    public AddJournalTransactionUseCase(IAddGenericJournalTransactionUseCase addGenericJournalTransactionUseCase)
    {
      _addGenericJournalTransactionUseCase = addGenericJournalTransactionUseCase;
    }

    public AddJournalTransactionResponseMessage AddJournalTransaction(
      AddJournalTransactionRequestMessage requestMessage)
    {
      List<TransactionEntry> transanctionEntryList = new List<TransactionEntry>();
      foreach (var entry in requestMessage.TransactionEntries)
      {
        var transactionEntry = new TransactionEntry
        {
          AccountHeadId = entry.AccountHeadId,
          CreditAmount = requestMessage.isContraTransanction? entry.DebitAmount : entry.CreditAmount,
          DebitAmount = requestMessage.isContraTransanction? entry.CreditAmount : entry.DebitAmount
        };
        transanctionEntryList.Add(transactionEntry);
      }

      var addGenericJournalTransactionRequestMessage = new AddGenericJournalTransactionRequestMessage
      {
          TransactionEntries = transanctionEntryList,
          BranchMediumId = requestMessage.BranchMediumId,
          CurrentUserName = requestMessage.CurrentUserName,
          Description = requestMessage.Description,
          SessionId = requestMessage.SessionId,
          TransactionDate = requestMessage.TransactionDate
      };
      var responseMessage = _addGenericJournalTransactionUseCase.AddGenericJournalTransaction(addGenericJournalTransactionRequestMessage);
      
      return new AddJournalTransactionResponseMessage
      {
        TransactionId = responseMessage.TransactionId,
        TransactionNo = responseMessage.TransactionNo
      };
    }
  }
}