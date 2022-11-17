using System.Linq;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Repositories
{
  public class UniqueAccountHeadPropertyChecker : IUniqueAccountHeadPropertyChecker
  {
    private readonly EmsDbContext _dbContext;

    public UniqueAccountHeadPropertyChecker(EmsDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public bool IsUniqueAccountCode(string accountCode)
    {
      return !_dbContext.AccountHeads.Any(a => a.AccountCode == accountCode);
    }

    public bool IsUniqueAccountHeadName(string accountHeadName)
    {
      return !_dbContext.AccountHeads.Any(a => a.AccountHeadName == accountHeadName);
    }

    public bool IsUniqueAccountHeadName(string accountHeadName, long accountHeadId)
    {
      return !_dbContext.AccountHeads.Any(a => a.AccountHeadName == accountHeadName && a.Id != accountHeadId);
    }

    public bool IsUniqueAccountCode(string accountCode, long accountHeadId)
    {
      return !_dbContext.AccountHeads.Any(a => a.AccountCode == accountCode && a.Id != accountHeadId);
    }

        public bool IsUniqueAccountCodeOfBranch(string accountCode, long branchMediumId, long sessionId)
        {
            return !_dbContext.BranchMediumAccountsHeads.Any(a => a.AccountCode == accountCode && a.BranchMedium.Id == branchMediumId && a.Session.Id == sessionId);
        }

        public bool IsUniqueAccountHeadNameOfBranch(string accountHeadName, long branchMediumId, long sessionId)
        {
            return !_dbContext.BranchMediumAccountsHeads.Any(a => a.AccountHeadName == accountHeadName && a.BranchMedium.Id == branchMediumId && a.Session.Id == sessionId);
        }
    }
}