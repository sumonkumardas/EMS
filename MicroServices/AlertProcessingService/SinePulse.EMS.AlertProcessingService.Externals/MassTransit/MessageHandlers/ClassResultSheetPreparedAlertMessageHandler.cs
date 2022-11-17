using System.Threading.Tasks;
using MassTransit;
using SinePulse.EMS.Messages.AlertProcessingMessages;
using Unity;

namespace SinePulse.EMS.AlertProcessingService.Externals.MassTransit.MessageHandlers
{
  public class ClassResultSheetPreparedAlertMessageHandler : IConsumer<ClassResultSheetPreparedAlertMessage>
  {
    private readonly IUnityContainer _container;

    public ClassResultSheetPreparedAlertMessageHandler(IUnityContainer container)
    {
      _container = container;
    }

    public virtual async Task Consume(ConsumeContext<ClassResultSheetPreparedAlertMessage> context)
    {
      var messageHandler =
        _container.Resolve<AlertProcessingService.MessageHandlers.ClassResultSheetPreparedAlertMessageHandler>();

      await messageHandler.Handle(context.Message);
    }
  }
}