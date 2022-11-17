using System.Threading.Tasks;
using SinePulse.EMS.AlertProcessingService.Services;
using SinePulse.EMS.Messages.AlertProcessingMessages;
using SinePulse.EMS.Utility.MessagingQueue;

namespace SinePulse.EMS.AlertProcessingService.MessageHandlers
{
  public class TermTestStartedAlertMessageHandler : IMessageHandler<TermTestStartedAlertMessage>
  {
    private readonly ITermTestStartedMessageSender _termTestStartedMessageSender;

    public TermTestStartedAlertMessageHandler(ITermTestStartedMessageSender termTestStartedMessageSender)
    {
      _termTestStartedMessageSender = termTestStartedMessageSender;
    }

    public async Task Handle(TermTestStartedAlertMessage message)
    {
      await _termTestStartedMessageSender.SendTermTestStartedMessage(message.TermTestId);
    }
  }
}