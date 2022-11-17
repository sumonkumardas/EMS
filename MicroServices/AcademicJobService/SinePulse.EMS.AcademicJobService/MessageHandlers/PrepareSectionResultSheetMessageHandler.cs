using System.Threading.Tasks;
using SinePulse.EMS.AcademicJobService.Services;
using SinePulse.EMS.Messages.AcademicJobMessages;
using SinePulse.EMS.Utility.MessagingQueue;

namespace SinePulse.EMS.AcademicJobService.MessageHandlers
{
  public class PrepareSectionResultSheetMessageHandler : IMessageHandler<PrepareSectionResultSheetMessage>
  {
    private readonly ISectionResultSheetPreparer _sectionResultSheetPreparer;

    public PrepareSectionResultSheetMessageHandler(ISectionResultSheetPreparer sectionResultSheetPreparer)
    {
      _sectionResultSheetPreparer = sectionResultSheetPreparer;
    }

    public async Task Handle(PrepareSectionResultSheetMessage message)
    {
      await _sectionResultSheetPreparer.PrepareSectionResultSheet(message.SectionId, message.TermId);
    }
  }
}