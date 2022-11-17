using System.Collections.Generic;
using FluentValidation.Results;

namespace SinePulse.EMS.Messages.SalarySheetMessages
{
  public class GetBankStatementResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public List<BankStatement> BankStatements { get;}

    public GetBankStatementResponseMessage(ValidationResult validationResult, List<BankStatement> bankStatements)
    {
      ValidationResult = validationResult;
      BankStatements = bankStatements;
    }
    public class BankStatement
    {
      public int SerialNo { get; set; }
      public string EmployeeName { get; set; }
      public string AccountNumber { get; set; }
      public decimal Amount { get; set; }
    }
  }
}