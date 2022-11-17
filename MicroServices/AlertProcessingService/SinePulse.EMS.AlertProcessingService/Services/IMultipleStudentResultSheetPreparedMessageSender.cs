using System.Collections.Generic;
using System.Threading.Tasks;

namespace SinePulse.EMS.AlertProcessingService.Services
{
  public interface IMultipleStudentResultSheetPreparedMessageSender
  {
    Task SendStudentResultSheetPreparedMessage(ICollection<long> studentSectionIds, long termId);
  }
}