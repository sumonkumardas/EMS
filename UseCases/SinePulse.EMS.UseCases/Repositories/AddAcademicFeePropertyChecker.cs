using System.Linq;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Accounts;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Repositories
{
  public class AddAcademicFeePropertyChecker : IAddAcademicFeePropertyChecker
  {
    private readonly EmsDbContext _dbContext;

    public AddAcademicFeePropertyChecker(EmsDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public bool IsValidFees(long branchMediumId, long sessionId, AcademicFeePeriodEnum academicFeePeriod,
      decimal[] feesCollection)
    {
      var accountHeads = _dbContext.BranchMediumAccountsHeads
        .Include(nameof(BranchMediumAccountHead.ParentHead))
        .Include(nameof(BranchMedium))
        .Where(a => a.BranchMedium.Id == branchMediumId &&
                    a.AccountHeadType == AccountHeadTypeEnum.Academic &&
                    (int) a.AccountPeriod == (int) academicFeePeriod &&
                    a.IsLedger)
        .ToList();
      return feesCollection != null && accountHeads.Count == feesCollection.Length;
    }
  }
}