using System.Threading.Tasks;

namespace SinePulse.EMS.AcademicJobService.Services
{
  public interface ISectionResultSheetPreparer
  {
    Task PrepareSectionResultSheet(long sectionId, long termId);
  }
}