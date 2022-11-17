using System.Threading.Tasks;

namespace SinePulse.EMS.AlertProcessingService.Services
{
  public interface IClassTestStartedMessageSender
  {
    Task SendClassTestStartedMessage(long classTestId);
  }
}