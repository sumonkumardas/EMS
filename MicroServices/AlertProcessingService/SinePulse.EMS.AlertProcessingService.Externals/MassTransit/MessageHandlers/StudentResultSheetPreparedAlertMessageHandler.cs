using System.Threading.Tasks;
using MassTransit;
using SinePulse.EMS.Messages.AlertProcessingMessages;
using Unity;

namespace SinePulse.EMS.AlertProcessingService.Externals.MassTransit.MessageHandlers
{
  public class StudentResultSheetPreparedAlertMessageHandler : IConsumer<StudentResultSheetPreparedAlertMessage>
  {
    private readonly IUnityContainer _container;

    public StudentResultSheetPreparedAlertMessageHandler(IUnityContainer container)
    {
      _container = container;
    }

    public virtual async Task Consume(ConsumeContext<StudentResultSheetPreparedAlertMessage> context)
    {
      var messageHandler =
        _container.Resolve<AlertProcessingService.MessageHandlers.StudentResultSheetPreparedAlertMessageHandler>();

      await messageHandler.Handle(context.Message);
    }
  }
}