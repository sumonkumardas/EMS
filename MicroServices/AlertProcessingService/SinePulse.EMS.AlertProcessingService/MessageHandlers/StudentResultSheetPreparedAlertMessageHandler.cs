using System.Threading.Tasks;
using SinePulse.EMS.AlertProcessingService.Services;
using SinePulse.EMS.Messages.AlertProcessingMessages;
using SinePulse.EMS.Utility.MessagingQueue;

namespace SinePulse.EMS.AlertProcessingService.MessageHandlers
{
  public class StudentResultSheetPreparedAlertMessageHandler : IMessageHandler<StudentResultSheetPreparedAlertMessage>
  {
    private readonly IStudentResultSheetPreparedMessageSender _studentResultSheetPreparedMessageSender;

    public StudentResultSheetPreparedAlertMessageHandler(
      IStudentResultSheetPreparedMessageSender studentResultSheetPreparedMessageSender)
    {
      _studentResultSheetPreparedMessageSender = studentResultSheetPreparedMessageSender;
    }

    public async Task Handle(StudentResultSheetPreparedAlertMessage message)
    {
      await _studentResultSheetPreparedMessageSender.SendStudentResultSheetPreparedMessage(message.StudentSectionId,
        message.TermId);
    }
  }
}