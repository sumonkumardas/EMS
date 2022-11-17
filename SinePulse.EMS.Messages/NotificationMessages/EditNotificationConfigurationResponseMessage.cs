using FluentValidation.Results;

namespace SinePulse.EMS.Messages.NotificationMessages
{
  public class EditNotificationConfigurationResponseMessage
  {
    public ValidationResult ValidationResult { get; }

    public EditNotificationConfigurationResponseMessage(ValidationResult validationResult)
    {
      ValidationResult = validationResult;
    }
  }
}