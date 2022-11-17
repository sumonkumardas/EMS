using System.Threading.Tasks;

namespace SinePulse.EMS.AlertProcessingService.Services
{
  public interface ITermStartedMessageSender
  {
    Task SendTermStartedMessage(long termId);
  }
}