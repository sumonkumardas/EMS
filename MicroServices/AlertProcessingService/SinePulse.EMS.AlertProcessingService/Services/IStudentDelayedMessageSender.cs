using System.Threading.Tasks;

namespace SinePulse.EMS.AlertProcessingService.Services
{
  public interface IStudentDelayedMessageSender
  {
    Task StudentDelayedMessage(long branchMediumId);
  }
}