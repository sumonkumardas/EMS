using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.ProjectPrimitives;

namespace SinePulse.EMS.UseCases.Repositories
{
  public interface IFeeCollectionValidationChecker
  {
    bool IsMonthlyFeesAlreadyCollected(MonthType month, long sessionId, long studentId, AcademicFeePeriodEnum feeType);
    bool IsYearlyFeesAlreadyCollected(long sessionId, long studentId, AcademicFeePeriodEnum feeType);
  }
}