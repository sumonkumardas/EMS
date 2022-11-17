using System.Linq;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Repositories
{
  public class UniqueSectionPropertyChecker : IUniqueSectionPropertyChecker
  {
    private readonly EmsDbContext _dbContext;

    public UniqueSectionPropertyChecker(EmsDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public bool IsUniqueSectionName(string sectionName)
    {
      return !_dbContext.Sections.Any(se => se.SectionName == sectionName);
    }
  }
}