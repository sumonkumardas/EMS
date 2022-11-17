using System.Threading.Tasks;
using MassTransit;
using SinePulse.EMS.Messages.AlertProcessingMessages;
using Unity;

namespace SinePulse.EMS.AlertProcessingService.Externals.MassTransit.MessageHandlers
{
  public class TermResultSheetPreparedAlertMessageHandler : IConsumer<TermResultSheetPreparedAlertMessage>
  {
    private readonly IUnityContainer _container;

    public TermResultSheetPreparedAlertMessageHandler(IUnityContainer container)
    {
      _container = container;
    }

    public virtual async Task Consume(ConsumeContext<TermResultSheetPreparedAlertMessage> context)
    {
      var messageHandler =
        _container.Resolve<AlertProcessingService.MessageHandlers.TermResultSheetPreparedAlertMessageHandler>();

      await messageHandler.Handle(context.Message);
    }
  }
}