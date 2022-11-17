using System.Linq;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Repositories
{
  public class BranchMediumAccountsHeadPropertyChecker : IBranchMediumAccountsHeadPropertyChecker
  {
    private readonly EmsDbContext _dbContext;

    public BranchMediumAccountsHeadPropertyChecker(EmsDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public bool IsAlreadyCOAImportedInSession(long sessionId)
    {
      return _dbContext.BranchMediumAccountsHeads.Any(a => a.Session.Id == sessionId);
    }

    public bool IsAlreadyCOAImportedInSession(long sessionId, long branchMediumId)
    {
      return _dbContext.BranchMediumAccountsHeads.Any(a =>
        a.Session.Id == sessionId && a.BranchMedium.Id == branchMediumId);
    }
  }
}