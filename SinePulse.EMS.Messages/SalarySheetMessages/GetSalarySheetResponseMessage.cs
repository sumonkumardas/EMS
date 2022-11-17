using System.Collections.Generic;
using FluentValidation.Results;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Messages.SalarySheetMessages
{
  public class GetSalarySheetResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public List<EmployeeSalary> EmployeeSalaries { get;}
    
    public GetSalarySheetResponseMessage(ValidationResult validationResult, List<EmployeeSalary> employeeSalaries)
    {
      ValidationResult = validationResult;
      EmployeeSalaries = employeeSalaries;
    }
    
    public class EmployeeSalary
    {
      public string EmployeeName { get; set; }
      public long EmployeeId { get; set; }
      public string EmployeeCode { get; set; }
      public string Designation { get; set; }
      public string BankAccountNo { get; set; }
      public decimal TotalAmount { get; set; }
      public List<SalaryComponent> SalaryComponents { get; set; }
    }

    public class SalaryComponent
    {
      public string ComponentName { get; set; }
      public decimal Amount { get; set; }
      public IncreaseDecreaseEnum ComponentType { get; set; }
    }
  }
}