using FluentValidation.Results;

namespace SinePulse.EMS.Messages.AccountHeadMessages
{
  public class AddAccountHeadResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public long AccountHeadId { get; }

    public AddAccountHeadResponseMessage(ValidationResult validationResult)
    {
      ValidationResult = validationResult;
    }

    public AddAccountHeadResponseMessage(ValidationResult validationResult, long accountHeadId)
    {
      ValidationResult = validationResult;
      AccountHeadId = accountHeadId;
    }
  }
}