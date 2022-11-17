using System.Threading.Tasks;
using SinePulse.EMS.AlertProcessingService.Services;
using SinePulse.EMS.Messages.AlertProcessingMessages;
using SinePulse.EMS.Utility.MessagingQueue;

namespace SinePulse.EMS.AlertProcessingService.MessageHandlers
{
  public class ClassTestStartedAlertMessageHandler : IMessageHandler<ClassTestStartedAlertMessage>
  {
    private readonly IClassTestStartedMessageSender _classTestStartedMessageSender;

    public ClassTestStartedAlertMessageHandler(IClassTestStartedMessageSender classTestStartedMessageSender)
    {
      _classTestStartedMessageSender = classTestStartedMessageSender;
    }

    public async Task Handle(ClassTestStartedAlertMessage message)
    {
      await _classTestStartedMessageSender.SendClassTestStartedMessage(message.ClassTestId);
    }
  }
}