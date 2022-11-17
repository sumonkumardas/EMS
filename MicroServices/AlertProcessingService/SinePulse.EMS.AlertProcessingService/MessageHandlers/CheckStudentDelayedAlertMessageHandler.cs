using System.Threading.Tasks;
using SinePulse.EMS.AlertProcessingService.Services;
using SinePulse.EMS.Messages.AlertProcessingMessages;
using SinePulse.EMS.Utility.MessagingQueue;

namespace SinePulse.EMS.AlertProcessingService.MessageHandlers
{
  public class CheckStudentDelayedAlertMessageHandler : IMessageHandler<CheckStudentDelayedAlertMessage>
  {
    private readonly IStudentDelayedMessageSender _studentDelayedMessageSender;

    public CheckStudentDelayedAlertMessageHandler(IStudentDelayedMessageSender studentDelayedMessageSender)
    {
      _studentDelayedMessageSender = studentDelayedMessageSender;
    }

    public async Task Handle(CheckStudentDelayedAlertMessage message)
    {
      await _studentDelayedMessageSender.StudentDelayedMessage(message.BranchMediumId);
    }
  }
}