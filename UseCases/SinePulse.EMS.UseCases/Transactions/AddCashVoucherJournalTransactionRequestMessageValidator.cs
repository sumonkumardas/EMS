using FluentValidation;
using SinePulse.EMS.Messages.TransactionMessages;

namespace SinePulse.EMS.UseCases.Transactions
{
  public class AddCashVoucherJournalTransactionRequestMessageValidator
    : AbstractValidator<AddCashVoucherJournalTransactionRequestMessage>
  {
  }
}