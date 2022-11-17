using System.Threading.Tasks;

namespace SinePulse.EMS.AlertProcessingService.Services
{
  public interface ITermResultSheetPreparedMessageSender
  {
    Task SendTermResultSheetPreparedMessage(long termId);
  }
}