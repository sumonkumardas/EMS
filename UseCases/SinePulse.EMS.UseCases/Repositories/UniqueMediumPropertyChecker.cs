using System.Linq;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Repositories
{
  public class UniqueMediumPropertyChecker : IUniqueMediumPropertyChecker
  {
    private readonly EmsDbContext _dbContext;

    public UniqueMediumPropertyChecker(EmsDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public bool IsUniqueMediumName(string mediumName)
    {
      return !_dbContext.Mediums.Any(m => m.MediumName == mediumName);
    }

    public bool IsUniqueMediumCode(string mediumCode)
    {
      return !_dbContext.Mediums.Any(m => m.MediumCode == mediumCode);
    }

    public bool IsUniqueMediumName(string mediumName, long mediumId)
    {
      return !_dbContext.Mediums.Any(m => m.MediumName == mediumName && m.Id != mediumId);
    }

    public bool IsUniqueMediumCode(string mediumCode, long mediumId)
    {
      return !_dbContext.Mediums.Any(m => m.MediumCode == mediumCode && m.Id != mediumId);
    }
  }
}