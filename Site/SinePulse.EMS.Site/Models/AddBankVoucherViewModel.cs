using System;
using System.Collections.Generic;
using MediatR;
using SinePulse.EMS.Messages.Shared;
using SinePulse.EMS.ProjectPrimitives;

namespace SinePulse.EMS.Site.Models
{
  public class AddBankVoucherViewModel : BaseViewModel
  {
    public long BranchMediumId { get; set; }
    public DateTime TransactionDate { get; set; }
    public string Description { get; set; }
    public long BankHeadId { get; set; }
    public TransactionEntryType TransactionEntryType { get; set; }
    public List<TransactionEntry> TransactionEntries { get; set; }
    public long BankAccountHeadId { get; set; }
    public List<BankAccountChildViewModel> BankAccountChilds { get; set; }
    public long SessionId { get; set; }

    public class TransactionEntry
    {
      public long AccountHeadId { get; set; }
      public decimal Amount { get; set; }
    }
  }
}