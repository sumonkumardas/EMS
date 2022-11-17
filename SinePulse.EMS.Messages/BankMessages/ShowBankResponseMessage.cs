using FluentValidation.Results;
using SinePulse.EMS.Messages.Model.Banks;

namespace SinePulse.EMS.Messages.BankMessages
{
  public class ShowBankResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public BankMessageModel Bank { get; }

    public ShowBankResponseMessage(ValidationResult validationResult, BankMessageModel bank)
    {
      ValidationResult = validationResult;
      Bank = bank;
    }
  }
}