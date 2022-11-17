using FluentValidation.Results;
using SinePulse.EMS.Domain.Banks;

namespace SinePulse.EMS.Messages.BankMessages
{
  public class GetBankStatementInfoResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public BankStatementInfo BankStatementInfos { get; }

    public GetBankStatementInfoResponseMessage(ValidationResult validationResult, BankStatementInfo bankStatementInfos)
    {
      ValidationResult = validationResult;
      BankStatementInfos = bankStatementInfos;
    }
    public class BankStatementInfo
    {
      public string BankName { get; set; }
      public string BankBranchName { get; set; }
      public string BankBranchAddress { get; set; }
      public string InstituteName { get; set; }
      public string BranchName { get; set; }
    }
  }
}