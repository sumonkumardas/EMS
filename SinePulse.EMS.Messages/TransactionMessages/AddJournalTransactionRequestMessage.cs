using System;
using System.Collections.Generic;
using MediatR;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.Messages.TransactionMessages
{
  public class AddJournalTransactionRequestMessage
    : IRequest<ValidatedData<AddJournalTransactionResponseMessage>>
  {
    public DateTime TransactionDate { get; set; }
    public string Description { get; set; }
    public List<TransactionEntry> TransactionEntries { get; set; }
    public long BranchMediumId { get; set; }
    public long SessionId { get; set; }
    
    public bool isContraTransanction { get; set; }
    public string CurrentUserName { get; set; }

    public class TransactionEntry
    {
      public long AccountHeadId { get; set; }

      public decimal DebitAmount { get; set; }
      public decimal CreditAmount { get; set; }
    }
  }
}