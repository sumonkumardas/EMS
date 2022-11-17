
using SinePulse.EMS.Messages.NotificationMessages;

namespace SinePulse.EMS.UseCases.Notifications
{
  public interface IEditNotificationConfigurationUseCase
  {
    void UpdateNotificationConfiguration(EditNotificationConfigurationRequestMessage message);
  }
}