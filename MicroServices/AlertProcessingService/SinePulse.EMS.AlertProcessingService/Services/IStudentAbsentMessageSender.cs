using System.Threading.Tasks;

namespace SinePulse.EMS.AlertProcessingService.Services
{
  public interface IStudentAbsentMessageSender
  {
    Task StudentAbsentMessage(long branchMediumId);
  }
}