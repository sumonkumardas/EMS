
using System.Threading.Tasks;
using Quartz;
using SinePulse.EMS.Constants;
using SinePulse.EMS.Messages.AlertProcessingMessages;

namespace SinePulse.EMS.BillingService.Jobs
{
  public class DueBillAlertMessageJob : IJob
  {

    public async Task Execute(IJobExecutionContext context)
    {
      var dataMap = context.MergedJobDataMap;
      var midNightTaskRunner = (IMidNightTaskRunner)dataMap["midNightTaskRunner"];
      await midNightTaskRunner.RunMidNightTask();
    }
  }
}
