using FluentValidation.Results;
using SinePulse.EMS.Messages.Model.Banks;

namespace SinePulse.EMS.Messages.BankAccountMessages
{
  public class AddBankAccountResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public BankAccountMessageModel BankAccount { get; }

    public AddBankAccountResponseMessage(ValidationResult validationResult, BankAccountMessageModel bankAccount)
    {
      ValidationResult = validationResult;
      BankAccount = bankAccount;
    }
  }
}