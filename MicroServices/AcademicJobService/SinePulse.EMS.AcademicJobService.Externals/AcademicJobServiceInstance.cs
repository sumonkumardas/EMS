using System.Threading.Tasks;
using System.Timers;
using MassTransit;

namespace SinePulse.EMS.AcademicJobService.Externals
{
  public class AcademicJobServiceInstance : AcademicJobService.AcademicJobServiceInstance
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