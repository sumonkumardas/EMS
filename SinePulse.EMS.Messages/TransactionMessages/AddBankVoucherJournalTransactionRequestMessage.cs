using System;
using System.Collections.Generic;
using MediatR;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.Messages.TransactionMessages
{
  public class AddBankVoucherJournalTransactionRequestMessage
    : IRequest<ValidatedData<AddBankVoucherJournalTransactionResponseMessage>>
  {
    public DateTime TransactionDate { get; set; }
    public string Description { get; set; }
    public List<TransactionEntry> TransactionEntries { get; set; }

    public bool IsDebitTransaction { get; set; }
    public long BankAccountHeadId { get; set; }
    public long BranchMediumId { get; set; }
    public long SessionId { get; set; }

    public string CurrentUserName { get; set; }

    public class TransactionEntry
    {
      public long AccountHeadId { get; set; }

      public decimal Amount { get; set; }
    }
  }
}