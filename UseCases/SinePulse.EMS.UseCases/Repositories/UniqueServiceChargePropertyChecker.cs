using SinePulse.EMS.Persistence.Facade;
using System;
using System.Linq;

namespace SinePulse.EMS.UseCases.Repositories
{
  public class UniqueServiceChargePropertyChecker : IUniqueServiceChargePropertyChecker
  {
    private readonly EmsDbContext _dbContext;

    public UniqueServiceChargePropertyChecker(EmsDbContext dbContext)
    {
      _dbContext = dbContext;
    }
    public bool IsUniqueEffectiveDate(DateTime effectiveDate, long branchMediumId)
    {
      return !_dbContext.ServiceCharges.Any(sc => sc.EffectiveDate.Date == effectiveDate.Date && sc.BranchMedium.Id == branchMediumId);
    }

    public bool IsUniqueEffectiveDate(DateTime effectiveDate, long branchMediumId, long serviceChargeId)
    {
      return !_dbContext.ServiceCharges.Any(sc => sc.EffectiveDate.Date == effectiveDate.Date && sc.BranchMedium.Id == branchMediumId && sc.Id != serviceChargeId);
    }
  }
}
