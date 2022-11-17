using System.Linq;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Repositories
{
  public class ValidBranchMediumChecker : IValidBranchMediumChecker
  {
    private readonly EmsDbContext _dbContext;

    public ValidBranchMediumChecker(EmsDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public bool IsValidBranchMedium(long branchMediumId)
    {
      return _dbContext.BranchMediums.Any(x => x.Id == branchMediumId);
    }
  }
}