using System.Threading.Tasks;
using SinePulse.EMS.AlertProcessingService.Services;
using SinePulse.EMS.Messages.AlertProcessingMessages;
using SinePulse.EMS.Utility.MessagingQueue;

namespace SinePulse.EMS.AlertProcessingService.MessageHandlers
{
  public class TermStartedAlertMessageHandler : IMessageHandler<TermStartedAlertMessage>
  {
    private readonly ITermStartedMessageSender _termStartedMessageSender;

    public TermStartedAlertMessageHandler(ITermStartedMessageSender termStartedMessageSender)
    {
      _termStartedMessageSender = termStartedMessageSender;
    }

    public async Task Handle(TermStartedAlertMessage message)
    {
      await _termStartedMessageSender.SendTermStartedMessage(message.TermId);
    }
  }
}