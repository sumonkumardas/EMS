using System.Threading.Tasks;
using MassTransit;
using SinePulse.EMS.Messages.AcademicJobMessages;
using Unity;

namespace SinePulse.EMS.AcademicJobService.Externals.MassTransit.MessageHandlers
{
  public class PrepareSectionResultSheetMessageHandler : IConsumer<PrepareSectionResultSheetMessage>
  {
    private readonly IUnityContainer _container;

    public PrepareSectionResultSheetMessageHandler(IUnityContainer container)
    {
      _container = container;
    }

    public virtual async Task Consume(ConsumeContext<PrepareSectionResultSheetMessage> context)
    {
      var messageHandler =
        _container.Resolve<AcademicJobService.MessageHandlers.PrepareSectionResultSheetMessageHandler>();

      await messageHandler.Handle(context.Message);
    }
  }
}