using System.Linq;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Repositories
{
  public class ValidSectionChecker : IValidSectionChecker
  {
    private readonly EmsDbContext _dbContext;

    public ValidSectionChecker(EmsDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public bool IsValidSection(long sectionId)
    {
      return _dbContext.Sections.Any(x => x.Id == sectionId);
    }
  }
}