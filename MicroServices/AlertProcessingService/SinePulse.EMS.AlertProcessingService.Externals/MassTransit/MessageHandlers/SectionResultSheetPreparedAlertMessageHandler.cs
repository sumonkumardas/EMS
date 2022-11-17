using System.Threading.Tasks;
using MassTransit;
using SinePulse.EMS.Messages.AlertProcessingMessages;
using Unity;

namespace SinePulse.EMS.AlertProcessingService.Externals.MassTransit.MessageHandlers
{
  public class SectionResultSheetPreparedAlertMessageHandler : IConsumer<SectionResultSheetPreparedAlertMessage>
  {
    private readonly IUnityContainer _container;

    public SectionResultSheetPreparedAlertMessageHandler(IUnityContainer container)
    {
      _container = container;
    }

    public virtual async Task Consume(ConsumeContext<SectionResultSheetPreparedAlertMessage> context)
    {
      var messageHandler =
        _container.Resolve<AlertProcessingService.MessageHandlers.SectionResultSheetPreparedAlertMessageHandler>();

      await messageHandler.Handle(context.Message);
    }
  }
}