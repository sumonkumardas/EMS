using System.Threading.Tasks;
using SinePulse.EMS.AlertProcessingService.Services;
using SinePulse.EMS.Messages.AlertProcessingMessages;
using SinePulse.EMS.Utility.MessagingQueue;

namespace SinePulse.EMS.AlertProcessingService.MessageHandlers
{
  public class TermResultSheetPreparedAlertMessageHandler : IMessageHandler<TermResultSheetPreparedAlertMessage>
  {
    private readonly ITermResultSheetPreparedMessageSender _termResultSheetPreparedMessageSender;

    public TermResultSheetPreparedAlertMessageHandler(
      ITermResultSheetPreparedMessageSender termResultSheetPreparedMessageSender)
    {
      _termResultSheetPreparedMessageSender = termResultSheetPreparedMessageSender;
    }

    public async Task Handle(TermResultSheetPreparedAlertMessage message)
    {
      await _termResultSheetPreparedMessageSender.SendTermResultSheetPreparedMessage(message.TermId);
    }
  }
}