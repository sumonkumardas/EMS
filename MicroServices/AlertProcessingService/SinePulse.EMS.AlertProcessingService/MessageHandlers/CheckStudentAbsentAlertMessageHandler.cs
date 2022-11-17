using System.Threading.Tasks;
using SinePulse.EMS.AlertProcessingService.Services;
using SinePulse.EMS.Messages.AlertProcessingMessages;
using SinePulse.EMS.Utility.MessagingQueue;

namespace SinePulse.EMS.AlertProcessingService.MessageHandlers
{
  public class CheckStudentAbsentAlertMessageHandler : IMessageHandler<CheckStudentAbsentAlertMessage>
  {
    private readonly IStudentAbsentMessageSender _studentAbsentMessageSender;

    public CheckStudentAbsentAlertMessageHandler(IStudentAbsentMessageSender studentAbsentMessageSender)
    {
      _studentAbsentMessageSender = studentAbsentMessageSender;
    }

    public async Task Handle(CheckStudentAbsentAlertMessage message)
    {
      await _studentAbsentMessageSender.StudentAbsentMessage(message.BranchMediumId);
    }
  }
}