using SinePulse.EMS.Messages.TransactionMessages;

namespace SinePulse.EMS.UseCases.Transactions
{
  public interface IAddCashVoucherJournalTransactionUseCase
  {
    AddCashVoucherJournalTransactionResponseMessage AddCashVoucherJournalTransaction(
      AddCashVoucherJournalTransactionRequestMessage requestMessage);
    AddGenericJournalTransactionRequestMessage GetGenericJournalTransactionRequestMessage(AddCashVoucherJournalTransactionRequestMessage requestMessage);
  }
}