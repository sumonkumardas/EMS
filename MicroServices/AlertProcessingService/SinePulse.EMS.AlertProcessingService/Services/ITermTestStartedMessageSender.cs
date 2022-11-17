using System.Threading.Tasks;

namespace SinePulse.EMS.AlertProcessingService.Services
{
  public interface ITermTestStartedMessageSender
  {
    Task SendTermTestStartedMessage(long termTestId);
  }
}