using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.UseCases.Repositories
{
  public interface IAddOptionalSubjectPropertyChecker
  {
    bool IsOptionalSubjectAlreadyAdded(long studentId, long subjectId, long classId, GroupEnum group);
  }
}