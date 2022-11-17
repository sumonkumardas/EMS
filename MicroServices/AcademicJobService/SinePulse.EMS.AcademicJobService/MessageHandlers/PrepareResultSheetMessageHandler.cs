using System.Threading.Tasks;
using SinePulse.EMS.AcademicJobService.Services;
using SinePulse.EMS.Messages.AcademicJobMessages;
using SinePulse.EMS.Utility.MessagingQueue;

namespace SinePulse.EMS.AcademicJobService.MessageHandlers
{
  public class PrepareResultSheetMessageHandler : IMessageHandler<PrepareResultSheetMessage>
  {
    private readonly IResultSheetPreparer _resultSheetPreparer;

    public PrepareResultSheetMessageHandler(IResultSheetPreparer resultSheetPreparer)
    {
      _resultSheetPreparer = resultSheetPreparer;
    }

    public async Task Handle(PrepareResultSheetMessage message)
    {
      await _resultSheetPreparer.PrepareResultSheet(message.StudentSectionId, message.TermId);
    }
  }
}