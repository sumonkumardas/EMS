using FluentValidation;
using SinePulse.EMS.Messages.NotificationMessages;

namespace SinePulse.EMS.UseCases.Notifications
{
  public class ShowNotificationConfigurationRequestMessageValidator : AbstractValidator<ShowNotificationConfigurationRequestMessage>
  {
    public ShowNotificationConfigurationRequestMessageValidator()
    {
    }
  }
}