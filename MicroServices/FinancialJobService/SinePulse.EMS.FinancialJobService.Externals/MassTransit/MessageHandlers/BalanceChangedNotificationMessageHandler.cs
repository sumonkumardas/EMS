using System.Threading.Tasks;
using MassTransit;
using SinePulse.EMS.Messages.FinancialJobMessages;
using Unity;

namespace SinePulse.EMS.FinancialJobService.Externals.MassTransit.MessageHandlers
{
  public class BalanceChangedNotificationMessageHandler : IConsumer<BalanceChangedNotificationMessage>
  {
    private readonly IUnityContainer _container;

    public BalanceChangedNotificationMessageHandler(IUnityContainer container)
    {
      _container = container;
    }

    public virtual async Task Consume(ConsumeContext<BalanceChangedNotificationMessage> context)
    {
      var messageHandler =
        _container.Resolve<FinancialJobService.MessageHandlers.BalanceChangedNotificationMessageHandler>();

      await messageHandler.Handle(context.Message);
    }
  }
}