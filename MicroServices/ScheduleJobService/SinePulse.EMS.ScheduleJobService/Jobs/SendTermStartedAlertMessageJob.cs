using System.Threading.Tasks;
using Quartz;
using SinePulse.EMS.Constants;
using SinePulse.EMS.Messages.AlertProcessingMessages;

namespace SinePulse.EMS.ScheduleJobService.Jobs
{
  public class SendTermStartedAlertMessageJob : IJob
  {
    public async Task Execute(IJobExecutionContext context)
    {
      var termId = context.JobDetail.JobDataMap.GetIntValue(Constants.TermId);

      var message = new TermStartedAlertMessage {TermId = termId};
      await StaticObjects.MessageSender.Send(message, MicroServiceAddresses.AlertProcessingService);
    }
  }
}