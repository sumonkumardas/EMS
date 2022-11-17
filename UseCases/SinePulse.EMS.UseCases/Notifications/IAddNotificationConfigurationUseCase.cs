using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Messages.NotificationMessages;

namespace SinePulse.EMS.UseCases.Notifications
{
  public interface IAddNotificationConfigurationUseCase
  {
    long AddNotificationConfiguration(AddNotificationConfigurationRequestMessage request);
  }
}