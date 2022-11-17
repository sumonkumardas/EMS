using System.Threading.Tasks;
using Quartz;
using SinePulse.EMS.Constants;
using SinePulse.EMS.Messages.AlertProcessingMessages;
using static SinePulse.EMS.ScheduleJobService.StaticObjects;

namespace SinePulse.EMS.ScheduleJobService.Jobs
{
  public class SendClassTestStartedAlertMessageJob : IJob
  {
    public async Task Execute(IJobExecutionContext context)
    {
      var classTestId = context.JobDetail.JobDataMap.GetIntValue(Constants.ClassTestId);
      var message = new ClassTestStartedAlertMessage {ClassTestId = classTestId};
      await MessageSender.Send(message, MicroServiceAddresses.AlertProcessingService);
    }
  }
}