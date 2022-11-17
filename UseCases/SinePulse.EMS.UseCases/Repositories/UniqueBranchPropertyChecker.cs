using System.Linq;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Repositories
{
  public class UniqueBranchPropertyChecker : IUniqueBranchPropertyChecker
  {
    private readonly EmsDbContext _dbContext;

    public UniqueBranchPropertyChecker(EmsDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public bool IsUniqueBranchName(string branchName)
    {
      return !_dbContext.Branches.Any(b => b.BranchName == branchName);
    }

    public bool IsUniqueBranchCode(string branchCode)
    {
      return !_dbContext.Branches.Any(b => b.BranchCode == branchCode);
    }

    public bool IsUniqueBranchName(string branchName, long branchId)
    {
      return !_dbContext.Branches.Any(b => b.BranchName == branchName && b.Id != branchId);
    }

    public bool IsUniqueBranchCode(string branchCode, long branchId)
    {
      return !_dbContext.Branches.Any(b => b.BranchCode == branchCode && b.Id != branchId);
    }
  }
}