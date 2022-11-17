using System.Threading.Tasks;
using MassTransit;
using SinePulse.EMS.Messages.AcademicJobMessages;
using Unity;

namespace SinePulse.EMS.AcademicJobService.Externals.MassTransit.MessageHandlers
{
  public class PrepareClassResultSheetMessageHandler : IConsumer<PrepareClassResultSheetMessage>
  {
    private readonly IUnityContainer _container;

    public PrepareClassResultSheetMessageHandler(IUnityContainer container)
    {
      _container = container;
    }

    public virtual async Task Consume(ConsumeContext<PrepareClassResultSheetMessage> context)
    {
      var messageHandler =
        _container.Resolve<AcademicJobService.MessageHandlers.PrepareClassResultSheetMessageHandler>();

      await messageHandler.Handle(context.Message);
    }
  }
}