using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Site.Models
{
    public class IncomeStatementViewModel : BaseViewModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public BranchMedium Branch { get; set; }
        public AccountHead RevenueAccountHead { get; set; }
        public AccountHead ExpenseAccountHead { get; set; }
        public string AccountTypeTreeUi { get; set; }
        public string DebitTreeUi { get; set; }
        public string CreditTreeUi { get; set; }
        public string EmptyTreeUi { get; set; }
        public string TotalExpense { get; set; }
        public string TotalRevenue { get; set; }
        public string NetIncome { get; set; }
        public class AccountHead
        {
            public long AccountHeadId { get; set; }
            public string AccountHeadName { get; set; }
            public decimal Amount { get; set; }
            public AccountTransactionTypeEnum TransactionEntryType { get; set; }
            public List<AccountHead> ChildAccountHeads { get; set; }
        }

        public class BranchMedium
        {
            public long BranchMediumId { get; set; }
            public string BranchMediumName { get; set; }
        }
    }
}
