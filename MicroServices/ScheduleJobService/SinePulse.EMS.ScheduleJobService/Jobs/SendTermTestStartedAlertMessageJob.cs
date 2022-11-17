using System.Threading.Tasks;
using Quartz;
using SinePulse.EMS.Constants;
using SinePulse.EMS.Messages.AlertProcessingMessages;

namespace SinePulse.EMS.ScheduleJobService.Jobs
{
  public class SendTermTestStartedAlertMessageJob : IJob
  {
    public async Task Execute(IJobExecutionContext context)
    {
      var termTestId = context.JobDetail.JobDataMap.GetIntValue(Constants.TermTestId);

      var message = new TermTestStartedAlertMessage {TermTestId = termTestId};
      await StaticObjects.MessageSender.Send(message, MicroServiceAddresses.AlertProcessingService);
    }
  }
}