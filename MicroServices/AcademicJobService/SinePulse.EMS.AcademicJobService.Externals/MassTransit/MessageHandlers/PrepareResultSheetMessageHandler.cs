using System.Threading.Tasks;
using MassTransit;
using SinePulse.EMS.Messages.AcademicJobMessages;
using Unity;

namespace SinePulse.EMS.AcademicJobService.Externals.MassTransit.MessageHandlers
{
  public class PrepareResultSheetMessageHandler : IConsumer<PrepareResultSheetMessage>
  {
    private readonly IUnityContainer _container;

    public PrepareResultSheetMessageHandler(IUnityContainer container)
    {
      _container = container;
    }

    public virtual async Task Consume(ConsumeContext<PrepareResultSheetMessage> context)
    {
      var messageHandler =
        _container.Resolve<AcademicJobService.MessageHandlers.PrepareResultSheetMessageHandler>();

      await messageHandler.Handle(context.Message);
    }
  }
}