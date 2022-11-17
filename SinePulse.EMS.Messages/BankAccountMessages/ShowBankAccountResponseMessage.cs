using FluentValidation.Results;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Messages.BankAccountMessages
{
  public class ShowBankAccountResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public AccountInfo AccountInfos { get; }

    public ShowBankAccountResponseMessage(ValidationResult validationResult, AccountInfo accountInfo)
    {
      ValidationResult = validationResult;
      AccountInfos = accountInfo;
    }
    
    public class AccountInfo
    {
      public string AccountNo { get; set; }
      public BankAccountTypeEnum AccountType { get; set; }
      public long BankBranchId { get; set; }
    }
  }
}