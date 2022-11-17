using FluentValidation.Results;
using MediatR;
using SinePulse.EMS.Messages.Model.Banks;

namespace SinePulse.EMS.Messages.BankMessages
{
  public class AddBankResponseMessage : IRequest<AddBankResponseMessage>
  {
    public ValidationResult ValidationResult { get; }
    public BankMessageModel Bank { get; }

    public AddBankResponseMessage(ValidationResult validationResult, BankMessageModel bank)
    {
      ValidationResult = validationResult;
      Bank = bank;
    }
  }
}