using SinePulse.EMS.Messages.TransactionMessages;

namespace SinePulse.EMS.UseCases.Transactions
{
  public interface IAddBankVoucherJournalTransactionUseCase
  {
    AddBankVoucherJournalTransactionResponseMessage AddBankVoucherJournalTransaction(
      AddBankVoucherJournalTransactionRequestMessage requestMessage);
    AddGenericJournalTransactionRequestMessage GetGenericJournalTransactionRequestMessage(
      AddBankVoucherJournalTransactionRequestMessage requestMessage);
  }
}