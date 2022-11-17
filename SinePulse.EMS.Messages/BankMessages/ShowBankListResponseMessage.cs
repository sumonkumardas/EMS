using System.Collections.Generic;
using FluentValidation.Results;

namespace SinePulse.EMS.Messages.BankMessages
{
  public class ShowBankListResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public List<Bank> BankInfos { get; }
    public ShowBankListResponseMessage(ValidationResult validationResult, List<Bank> bankInfos)
    {
      ValidationResult = validationResult;
      BankInfos = bankInfos;
    }

    public class Bank
    {
      public long BankId { get; set; }
      public long BankBranchId { get; set; }
      public string BankName { get; set; }
      public string BankBranchName { get; set; }
      public string AccountNumber { get; set; }
    }
  }
}