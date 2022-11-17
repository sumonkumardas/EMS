using System.Linq;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Repositories
{
  public class ValidClassTestChecker : IValidClassTestChecker
  {
    private readonly EmsDbContext _dbContext;

    public ValidClassTestChecker(EmsDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public bool IsValidClassTest(long classTestId)
    {
      return _dbContext.ClassTests.Any(x => x.Id == classTestId && x.Section != null);
    }
  }
}