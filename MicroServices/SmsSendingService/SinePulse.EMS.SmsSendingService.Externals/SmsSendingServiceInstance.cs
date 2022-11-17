using System.Threading.Tasks;
using MassTransit;

namespace SinePulse.EMS.SmsSendingService.Externals
{
  public class SmsSendingServiceInstance : SmsSendingService.SmsSendingServiceInstance
  {
    private IBusControl _bus;

    public override async Task Start()
    {
      var objectGraph = new ObjectGraph();
      await objectGraph.Initialize();

      _bus = objectGraph.Bus;

      await _bus.StartAsync();
    }

    public override async Task Stop()
    {
      await _bus.StopAsync();
    }
  }
}