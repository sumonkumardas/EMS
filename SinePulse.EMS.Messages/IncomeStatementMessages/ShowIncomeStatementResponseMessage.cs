using System.Collections.Generic;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Messages.IncomeStatementMessages
{
  public class ShowIncomeStatementResponseMessage
  {
    public AccountHead RevenueAccountHead { get; set; }

    public AccountHead ExpenseAccountHead { get; set; }

    public decimal TotalExpense { get; set; }

    public decimal TotalRevenue { get; set; }
    public decimal NetIncome { get; set; }

    public class AccountHead
    {
      public long AccountHeadId { get; set; }

      public string AccountHeadName { get; set; }

      public decimal Balance { get; set; }

      public decimal Debit { get; set; }

      public decimal Credit { get; set; }

      public AccountTransactionTypeEnum TransactionEntryType { get; set; }

      public List<AccountHead> AccountHeads { get; set; }
    }
  }
}