using System.Collections.Generic;
using SinePulse.EMS.Messages.EmployeeSalaryMessages;
using SinePulse.EMS.Messages.Model.Accounts;
using SinePulse.EMS.ProjectPrimitives;

namespace SinePulse.EMS.Site.Models
{
  public class SaveSalarySheetViewModel : BaseViewModel
  {
    public long BranchMediumId { get; set; }
    public List<int> Years { get; set; }
    public int Year { get; set; }
    public MonthType Month { get; set; }
    public decimal[] TotalAmounts { get; set; }
    public decimal TotalTaxDeduction { get; set; }
    public List<decimal> SalaryComponentAmounts { get; set; }
    public List<string> SalaryComponentNames { get; set; }
    public List<long> EmployeeIds { get; set; }
    public List<string> EmployeeBankAccounts { get; set; }
    public decimal[] OtherDeductionAmounts { get; set; }

    public string BankAccount { get; set; }
    public IEnumerable<BranchMediumAccountsHeadMessageModel> BankAccounts { get; set; } =
      new List<BranchMediumAccountsHeadMessageModel>();
  }
}