using FluentValidation.Results;

namespace SinePulse.EMS.Messages.LoginUserMessages
{
  public class AddLoginUserResponseMessage
  {
    public ValidationResult ValidationResult { get; }

    public AddLoginUserResponseMessage(ValidationResult validationResult)
    {
      ValidationResult = validationResult;
    }
  }
}