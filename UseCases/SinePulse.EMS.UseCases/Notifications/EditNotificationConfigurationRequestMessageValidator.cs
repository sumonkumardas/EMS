using FluentValidation;
using SinePulse.EMS.Messages.NotificationMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.Notifications
{
  public class EditNotificationConfigurationRequestMessageValidator : AbstractValidator<EditNotificationConfigurationRequestMessage>
  {

    public EditNotificationConfigurationRequestMessageValidator()
    {
     
    }
  }
}