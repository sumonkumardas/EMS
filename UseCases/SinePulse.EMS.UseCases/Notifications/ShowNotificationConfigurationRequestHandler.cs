using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.NotificationMessages;
using SinePulse.EMS.UseCases.Notifications;

namespace SinePulse.EMS.UseCases.Notifications
{
  public class ShowNotificationConfigurationRequestHandler : IRequestHandler<ShowNotificationConfigurationRequestMessage, ShowNotificationConfigurationResponseMessage>
  {
    private readonly ShowNotificationConfigurationRequestMessageValidator _validator;
    private readonly IShowNotificationConfigurationUseCase _useCase;

    public ShowNotificationConfigurationRequestHandler(ShowNotificationConfigurationRequestMessageValidator validator, IShowNotificationConfigurationUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ShowNotificationConfigurationResponseMessage> Handle(ShowNotificationConfigurationRequestMessage request,
      CancellationToken cancellationToken)
    {
      ShowNotificationConfigurationResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ShowNotificationConfigurationResponseMessage(validationResult,null);
        return Task.FromResult(returnMessage);
      }

      var notificationConfiguration = _useCase.ShowNotificationConfiguration(request);

      returnMessage = new ShowNotificationConfigurationResponseMessage(validationResult, notificationConfiguration);
      return Task.FromResult(returnMessage);
    }
  }
}