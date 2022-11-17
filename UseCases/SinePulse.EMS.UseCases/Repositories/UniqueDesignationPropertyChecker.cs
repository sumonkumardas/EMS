using System.Linq;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Repositories
{
  public class UniqueDesignationPropertyChecker : IUniqueDesignationPropertyChecker
  {
    private readonly EmsDbContext _dbContext;

    public UniqueDesignationPropertyChecker(EmsDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public bool IsUniqueDesignationName(string designationName)
    {
      return !_dbContext.Designations.Any(d => d.DesignationName == designationName);
    }

    public bool IsUniqueDesignationName(string designationName, long designationId)
    {
      return !_dbContext.Designations.Any(d => d.DesignationName == designationName && d.Id != designationId);
    }
  }
}