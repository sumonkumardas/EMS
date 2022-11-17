using MediatR;
using SinePulse.EMS.Domain.Academic;

namespace SinePulse.EMS.Messages.NotificationMessages
{
  public class ShowNotificationConfigurationRequestMessage : IRequest<ShowNotificationConfigurationResponseMessage>
  {
    public long BranchMediumId { get; set; }
  }
}