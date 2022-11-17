using System.Threading.Tasks;

namespace SinePulse.EMS.AlertProcessingService.Services
{
  public interface IStudentResultSheetPreparedMessageSender
  {
    Task SendStudentResultSheetPreparedMessage(long studentSectionId, long termId);
  }
}