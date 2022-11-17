using SinePulse.EMS.Domain.Transactions;
using SinePulse.EMS.Messages.TransactionMessages;
using System.Threading.Tasks;

namespace SinePulse.EMS.UseCases.Transactions
{
  public interface IAddGenericJournalTransactionUseCase
  {
    AddGenericJournalTransactionResponseMessage AddGenericJournalTransaction(
      AddGenericJournalTransactionRequestMessage requestMessage);
    Transaction AddTransaction(AddGenericJournalTransactionRequestMessage requestMessage);
    Task SendBalanceChangedNotificationMessages(Transaction transaction);
  }
}