using System.Threading.Tasks;
using SinePulse.EMS.AcademicJobService.Services;
using SinePulse.EMS.Messages.AcademicJobMessages;
using SinePulse.EMS.Utility.MessagingQueue;

namespace SinePulse.EMS.AcademicJobService.MessageHandlers
{
  public class PrepareClassResultSheetMessageHandler : IMessageHandler<PrepareClassResultSheetMessage>
  {
    private readonly IClassResultSheetPreparer _classResultSheetPreparer;

    public PrepareClassResultSheetMessageHandler(IClassResultSheetPreparer classResultSheetPreparer)
    {
      _classResultSheetPreparer = classResultSheetPreparer;
    }

    public async Task Handle(PrepareClassResultSheetMessage message)
    {
      await _classResultSheetPreparer.PrepareClassResultSheet(message.ClassId, message.TermId);
    }
  }
}