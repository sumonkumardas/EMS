using System.Linq;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Repositories
{
  public class UniqueJobTypePropertyChecker : IUniqueJobTypePropertyChecker
  {
    private readonly EmsDbContext _dbContext;

    public UniqueJobTypePropertyChecker(EmsDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public bool IsUniqueJobTypeTitle(string title)
    {
      return !_dbContext.JobTypes.Any(j => j.JobTitle == title);
    }

    public bool IsUniqueJobTypeTitle(string title, long jobTypeId)
    {
      return !_dbContext.JobTypes.Any(j => j.JobTitle == title && j.Id != jobTypeId);
    }
  }
}