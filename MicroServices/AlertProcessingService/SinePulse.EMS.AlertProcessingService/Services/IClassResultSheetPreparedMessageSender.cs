using System.Threading.Tasks;

namespace SinePulse.EMS.AlertProcessingService.Services
{
  public interface IClassResultSheetPreparedMessageSender
  {
    Task SendClassResultSheetPreparedMessage(long classId, long termId);
  }
}