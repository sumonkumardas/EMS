using System.Linq;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Repositories
{
  public class ValidTermTestChecker : IValidTermTestChecker
  {
    private readonly EmsDbContext _dbContext;

    public ValidTermTestChecker(EmsDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public bool IsValidTermTest(long testId)
    {
      return _dbContext.TermTests.Any(x => x.Id == testId);
    }
  }
}