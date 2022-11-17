using FluentValidation.Results;

namespace SinePulse.EMS.Messages.AccountHeadMessages
{
  public class EditAccountHeadResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public long AccountHeadId { get; }

    public EditAccountHeadResponseMessage(ValidationResult validationResult)
    {
      ValidationResult = validationResult;
    }

    public EditAccountHeadResponseMessage(ValidationResult validationResult, long accountHeadId)
    {
      ValidationResult = validationResult;
      AccountHeadId = accountHeadId;
    } 
  }
}