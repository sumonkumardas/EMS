using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.NotificationMessages;
using SinePulse.EMS.UseCases.Notifications;
using SinePulse.EMS.UseCases.Notifications;

namespace SinePulse.EMS.UseCases.Notifications
{
  public class EditNotificationConfigurationRequestHandler : IRequestHandler<EditNotificationConfigurationRequestMessage, EditNotificationConfigurationResponseMessage>
  {
    private readonly EditNotificationConfigurationRequestMessageValidator _validator;
    private readonly IEditNotificationConfigurationUseCase _useCase;

    public EditNotificationConfigurationRequestHandler(EditNotificationConfigurationRequestMessageValidator validator, IEditNotificationConfigurationUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<EditNotificationConfigurationResponseMessage> Handle(EditNotificationConfigurationRequestMessage request,
      CancellationToken cancellationToken)
    {
      EditNotificationConfigurationResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new EditNotificationConfigurationResponseMessage(validationResult);
        return Task.FromResult(returnMessage);
      }

      _useCase.UpdateNotificationConfiguration(request);

      returnMessage = new EditNotificationConfigurationResponseMessage(validationResult);
      return Task.FromResult(returnMessage);
    }
  }
}