using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.NotificationMessages;

namespace SinePulse.EMS.UseCases.Notifications
{
  public class AddNotificationConfigurationRequestHandler : IRequestHandler<AddNotificationConfigurationRequestMessage, AddNotificationConfigurationResponseMessage>
  {
    private readonly AddNotificationConfigurationRequestMessageValidator _validator;
    private readonly IAddNotificationConfigurationUseCase _useCase;

    public AddNotificationConfigurationRequestHandler(AddNotificationConfigurationRequestMessageValidator validator, IAddNotificationConfigurationUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<AddNotificationConfigurationResponseMessage> Handle(AddNotificationConfigurationRequestMessage request, CancellationToken cancellationToken)
    {
      AddNotificationConfigurationResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new AddNotificationConfigurationResponseMessage(validationResult);
        return Task.FromResult(returnMessage);
      }

      var notificationConfigurationId = _useCase.AddNotificationConfiguration(request);

      returnMessage = new AddNotificationConfigurationResponseMessage(validationResult, notificationConfigurationId);
      return Task.FromResult(returnMessage);
    }
  }
}