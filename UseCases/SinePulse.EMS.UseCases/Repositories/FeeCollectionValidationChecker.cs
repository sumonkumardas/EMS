using System.Linq;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.ProjectPrimitives;

namespace SinePulse.EMS.UseCases.Repositories
{
  public class FeeCollectionValidationChecker : IFeeCollectionValidationChecker
  {
    private readonly EmsDbContext _dbContext;

    public FeeCollectionValidationChecker(EmsDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public bool IsMonthlyFeesAlreadyCollected(MonthType month, long sessionId, long studentId, AcademicFeePeriodEnum feeType)
    {
      if (feeType == AcademicFeePeriodEnum.Yearly)
        return true;
      return !_dbContext.FeeCollections.Any(f =>
        f.Month == month && 
        f.Session.Id == sessionId && 
        f.Student.Id == studentId &&
        f.FeeType == feeType);
    }

    public bool IsYearlyFeesAlreadyCollected(long sessionId, long studentId, AcademicFeePeriodEnum feeType)
    {
      if (feeType == AcademicFeePeriodEnum.Monthly)
        return true;
      return !_dbContext.FeeCollections.Any(f =>
        f.Session.Id == sessionId && 
        f.Student.Id == studentId &&
        f.FeeType == feeType);
    }
  }
}