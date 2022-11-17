using System.Collections.Generic;
using MediatR;
using SinePulse.EMS.Messages.GenerateSalarySheetMessages;

namespace SinePulse.EMS.Messages.SalarySheetMessages
{
  public class SaveSalarySheetRequestMessage : IRequest<SaveSalarySheetResponseMessage>
  {
    public long BranchMediumId { get; set; }
    public int Year { get; set; }
    public int Month { get; set; }
    public List<decimal> SalaryComponentAmounts { get; set; }
    public List<string> SalaryComponentNames { get; set; }
    public decimal TotalTaxDeduction { get; set; }
    public decimal TotalAmount { get; set; }
    public string CurrentUserName { get; set; }
    public List<long> EmployeeIds { get; set; }
    public List<string> EmployeeBankAccounts { get; set; }
    public decimal[] OtherDeductionAmounts { get; set; }
  }
}