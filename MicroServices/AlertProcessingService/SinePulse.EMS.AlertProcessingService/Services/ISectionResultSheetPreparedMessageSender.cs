using System.Threading.Tasks;

namespace SinePulse.EMS.AlertProcessingService.Services
{
  public interface ISectionResultSheetPreparedMessageSender
  {
    Task SendSectionResultSheetPreparedMessage(long sectionId, long termId);
  }
}