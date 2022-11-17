using System.Threading.Tasks;
using SinePulse.EMS.AlertProcessingService.Services;
using SinePulse.EMS.Messages.AlertProcessingMessages;
using SinePulse.EMS.Utility.MessagingQueue;

namespace SinePulse.EMS.AlertProcessingService.MessageHandlers
{
  public class SectionResultSheetPreparedAlertMessageHandler : IMessageHandler<SectionResultSheetPreparedAlertMessage>
  {
    private readonly ISectionResultSheetPreparedMessageSender _classResultSheetPreparedMessageSender;

    public SectionResultSheetPreparedAlertMessageHandler(
      ISectionResultSheetPreparedMessageSender classResultSheetPreparedMessageSender)
    {
      _classResultSheetPreparedMessageSender = classResultSheetPreparedMessageSender;
    }

    public async Task Handle(SectionResultSheetPreparedAlertMessage message)
    {
      await _classResultSheetPreparedMessageSender.SendSectionResultSheetPreparedMessage(message.SectionId,
        message.TermId);
    }
  }
}