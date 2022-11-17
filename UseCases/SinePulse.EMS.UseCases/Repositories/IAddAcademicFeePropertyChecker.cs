using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.UseCases.Repositories
{
  public interface IAddAcademicFeePropertyChecker
  {
    bool IsValidFees(long branchMediumId, long sessionId, AcademicFeePeriodEnum academicFeePeriod,
      decimal[] feesCollection);
  }
}