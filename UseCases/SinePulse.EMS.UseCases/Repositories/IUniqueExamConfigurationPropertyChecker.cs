using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.UseCases.Repositories
{
  public interface IUniqueExamConfigurationPropertyChecker
  {
    bool IsUniqueExamConfigurationMark(long classId, long groupId, long subjectId,long termId);
    bool IsUniqueExamConfigurationMark(long classId, long groupId, long subjectId, long termId,long examConfigurationId);
    bool IsMarksAlreadyAdded(long examConfigurationId);
  }
}