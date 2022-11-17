using System;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.UseCases.Repositories
{
  public interface ITermTestOverlappingChecker
  {
    bool IsOnBetweenValidTermDate(DateTime routineDate, long termId);

    bool IsNonOverlappingStartTime(DateTime routineDate, TimeSpan startTime, long termId, long classId,
      GroupEnum groupId, long subjectId);

    bool IsNonOverlappingEndTime(DateTime routineDate, TimeSpan startTime, long termId, long classId, GroupEnum groupId,
      long subjectId);

    bool IsNonOverlappingStartTime(DateTime routineDate, TimeSpan startTime, long termId, long classId,
      GroupEnum groupId, long subjectId,long termTestId);

    bool IsNonOverlappingEndTime(DateTime routineDate, TimeSpan startTime, long termId, long classId, GroupEnum groupId,
      long subjectId, long termTestId);

    bool IsTermTestForSubjectAlreadyScheduled(long classId, long subjectId, long termId, ExamTypeEnum examType);
    bool IsTermTestForSubjectAlreadyScheduled(long classId, long subjectId, long termId, ExamTypeEnum examType,long termTestId);
  }
}