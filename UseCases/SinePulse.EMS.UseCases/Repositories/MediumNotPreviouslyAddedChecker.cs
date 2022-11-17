using System.Linq;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Repositories
{
  public class MediumNotPreviouslyAddedChecker : IMediumNotPreviouslyAddedChecker
  {
    private readonly EmsDbContext _dbContext;

    public MediumNotPreviouslyAddedChecker(EmsDbContext dbContext)
    {
      _dbContext = dbContext;
    }
    public bool IsMediumPreviouslyAdded(long mediumId, long branchId)
    {
      return !_dbContext.BranchMediums.Any(s => s.Medium.Id == mediumId && s.Branch.Id == branchId);
    }

    public bool IsMediumPreviouslyAdded(long mediumId, long branchId, long branchMediumId)
    {
      return !_dbContext.BranchMediums.Any(s => s.Medium.Id == mediumId && s.Branch.Id == branchId && s.Id != branchMediumId);
    }
  }
}