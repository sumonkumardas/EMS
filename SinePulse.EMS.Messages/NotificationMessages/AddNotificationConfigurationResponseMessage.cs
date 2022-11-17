using FluentValidation.Results;

namespace SinePulse.EMS.Messages.NotificationMessages
{
  public class AddNotificationConfigurationResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public long ConfigurationId { get; }

    public AddNotificationConfigurationResponseMessage(ValidationResult validationResult)
    {
      ValidationResult = validationResult;
    }
    
    public AddNotificationConfigurationResponseMessage(ValidationResult validationResult, long configurationId)
    {
      ValidationResult = validationResult;
      ConfigurationId = configurationId;
    }
  }
}