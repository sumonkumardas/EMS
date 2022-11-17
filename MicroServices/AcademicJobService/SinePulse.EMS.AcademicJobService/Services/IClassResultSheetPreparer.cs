using System.Threading.Tasks;

namespace SinePulse.EMS.AcademicJobService.Services
{
  public interface IClassResultSheetPreparer
  {
    Task PrepareClassResultSheet(long classId, long termId);
  }
}