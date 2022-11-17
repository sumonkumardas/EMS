using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.ProjectPrimitives;

namespace SinePulse.EMS.Site.Models
{
  public class AccountDisplayModel : BaseViewModel
  {
    public BranchMedium AccountDisplayBranchMedium { get; set; }
    public Institute AccountDisplayInstitute { get; set; }
    public Branch AccountDisplayBranch { get; set; }
    public TrialBalanceViewModel TrialBalanceViewModel { get; set; }
    public Session CurrentSession { get; set; }
    public List<Session> Sessions { get; set; }
    public MonthType Month { get; set; }
    public IncomeStatementViewModel IncomeStatementViewModel { get; set; }
    public BalanceSheetViewModel BalanceSheetViewModel { get; set; }
    public AddAccoundHeadChildViewModel AddAccoundHeadChildViewModel { get; set; }
    public List<TransactionViewModel> Transactions { get; set; }

    public class Session
    {
      public long SessionId { get; set; }
      public string SessionName { get; set; }
      public DateTime StartDate { get; set; }
      public DateTime EndDate { get; set; }
    }

    public class Institute
    {
      public long InstituteId { get; set; }
      public string InstituteName { get; set; }
    }

    public class Branch
    {
      public long BranchId { get; set; }
      public string BranchName { get; set; }
    }

    public class BranchMedium
    {
      public long BranchMediumId { get; set; }
      public string BranchMediumName { get; set; }
      public string ShiftName { get; set; }
    }
  }
}
