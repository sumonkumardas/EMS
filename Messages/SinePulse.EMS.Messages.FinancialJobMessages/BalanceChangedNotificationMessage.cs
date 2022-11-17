using System;
using SinePulse.EMS.InterServiceMessages;

namespace SinePulse.EMS.Messages.FinancialJobMessages
{
  public class BalanceChangedNotificationMessage : IMessage
  {
    public decimal ChangedAmount { get; set; }
    public DateTime Date { get; set; }
    public long BranchMediumAccountsHeadId { get; set; }
  }
}