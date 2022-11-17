using System.Threading.Tasks;
using Quartz;
using SinePulse.EMS.Constants;
using SinePulse.EMS.Messages.AlertProcessingMessages;

namespace SinePulse.EMS.ScheduleJobService.Jobs
{
  public class SendCheckStudentAbsentAlertMessageJob : IJob
  {
    public async Task Execute(IJobExecutionContext context)
    {
      var branchMediumId = context.JobDetail.JobDataMap.GetIntValue(Constants.BranchMediumId);

      var message = new CheckStudentAbsentAlertMessage {BranchMediumId = branchMediumId};
      await StaticObjects.MessageSender.Send(message, MicroServiceAddresses.AlertProcessingService);
    }
  }
}