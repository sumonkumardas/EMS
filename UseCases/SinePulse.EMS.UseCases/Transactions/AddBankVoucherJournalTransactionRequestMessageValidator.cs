using FluentValidation;
using SinePulse.EMS.Messages.TransactionMessages;

namespace SinePulse.EMS.UseCases.Transactions
{
  public class AddBankVoucherJournalTransactionRequestMessageValidator
    : AbstractValidator<AddBankVoucherJournalTransactionRequestMessage>
  {
  }
}