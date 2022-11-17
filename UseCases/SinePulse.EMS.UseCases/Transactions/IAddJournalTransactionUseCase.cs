using SinePulse.EMS.Messages.TransactionMessages;

namespace SinePulse.EMS.UseCases.Transactions
{
  public interface IAddJournalTransactionUseCase
  {
    AddJournalTransactionResponseMessage AddJournalTransaction(
      AddJournalTransactionRequestMessage requestMessage);
  }
}