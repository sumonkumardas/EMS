using System.Threading.Tasks;
using SinePulse.EMS.AlertProcessingService.Services;
using SinePulse.EMS.Messages.AlertProcessingMessages;
using SinePulse.EMS.Utility.MessagingQueue;

namespace SinePulse.EMS.AlertProcessingService.MessageHandlers
{
  public class ClassResultSheetPreparedAlertMessageHandler : IMessageHandler<ClassResultSheetPreparedAlertMessage>
  {
    private readonly IClassResultSheetPreparedMessageSender _classResultSheetPreparedMessageSender;

    public ClassResultSheetPreparedAlertMessageHandler(
      IClassResultSheetPreparedMessageSender classResultSheetPreparedMessageSender)
    {
      _classResultSheetPreparedMessageSender = classResultSheetPreparedMessageSender;
    }

    public async Task Handle(ClassResultSheetPreparedAlertMessage message)
    {
      await _classResultSheetPreparedMessageSender.SendClassResultSheetPreparedMessage(message.ClassId, message.TermId);
    }
  }
}