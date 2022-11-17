using System.Threading.Tasks;

namespace SinePulse.EMS.AcademicJobService.Services
{
  public interface IResultSheetPreparer
  {
    Task PrepareResultSheet(long studentSectionId, long termId);
  }
}