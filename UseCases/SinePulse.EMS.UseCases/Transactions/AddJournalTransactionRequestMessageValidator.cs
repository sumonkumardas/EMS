using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using SinePulse.EMS.Messages.TransactionMessages;

namespace SinePulse.EMS.UseCases.Transactions
{
  public class AddJournalTransactionRequestMessageValidator
    : AbstractValidator<AddJournalTransactionRequestMessage>
  {
    public AddJournalTransactionRequestMessageValidator()
    {
      RuleFor(x => x.TransactionEntries).Must(HaveExactlyOnePositiveDebitOrCreditAmount)
        .WithMessage("Every transaction entry have exactly one positive debit or credit amount");
      RuleFor(x => x.TransactionEntries).Must(HaveBalanceDebitCredit)
        .WithMessage("Total Amount of Debit and Credit Must be equal");
    }

    private static bool HaveExactlyOnePositiveDebitOrCreditAmount(
      List<AddJournalTransactionRequestMessage.TransactionEntry> transactionEntries)
    {
      return transactionEntries.All(HaveExactlyOnePositiveDebitOrCreditAmount);
    }

    private static bool HaveExactlyOnePositiveDebitOrCreditAmount(
      AddJournalTransactionRequestMessage.TransactionEntry transactionEntry)
    {
      return transactionEntry.DebitAmount == 0 && transactionEntry.CreditAmount > 0
             || transactionEntry.CreditAmount == 0 && transactionEntry.DebitAmount > 0;
    }

    private static bool HaveBalanceDebitCredit(
      List<AddJournalTransactionRequestMessage.TransactionEntry> transactionEntries)
    {
      var totalDebit = transactionEntries.Sum(e => e.DebitAmount);
      var totalCredit = transactionEntries.Sum(e => e.CreditAmount);

      return totalDebit == totalCredit;
    }
  }
}