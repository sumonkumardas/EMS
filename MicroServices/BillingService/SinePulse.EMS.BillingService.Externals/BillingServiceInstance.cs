

using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Quartz;
using System.Timers;
using log4net;

namespace SinePulse.EMS.BillingService.Externals
{
  public class BillingServiceInstance: SinePulse.EMS.BillingService.BillingServiceInstance
  {
    private IScheduler _scheduler;
    private readonly ILog _logger;

    public BillingServiceInstance(ILog logger)
    {
      _logger = logger;
    }

    public override async Task Start()
    {
      var objectGraph = new ObjectGraph();
      await objectGraph.Initialize(_logger); 
      _scheduler = objectGraph.Scheduler;
      await objectGraph.BillingJobServiceApplication.Run();
      await _scheduler.Start();
      _logger.Info("Billing Service Scheduler started.");
    }

    public override async Task Stop()
    {
      await _scheduler.Shutdown(true);
      _logger.Info("Billing Service Scheduler stopped.");
    }
  }
}
