using FluentValidation.Results;

namespace SinePulse.EMS.Messages.LoginUserMessages
{
  public class ChangeLoginUserPasswordResponseMessage
  {
    public ValidationResult ValidationResult { get; }

    public ChangeLoginUserPasswordResponseMessage(ValidationResult validationResult)
    {
      ValidationResult = validationResult;
    }
  }
}