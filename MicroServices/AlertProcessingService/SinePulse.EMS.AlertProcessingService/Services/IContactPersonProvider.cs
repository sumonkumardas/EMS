using System.Collections.Generic;
using System.Threading.Tasks;
using SinePulse.EMS.AlertProcessingService.Data;

namespace SinePulse.EMS.AlertProcessingService.Services
{
  public interface IContactPersonProvider
  {
    Task<ICollection<ContactPersonData>> GetContactPersons(long studentId);
  }
}