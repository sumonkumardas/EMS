using System.Linq;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Repositories
{
  public class ValidTermChecker : IValidTermChecker
  {
    private readonly EmsDbContext _dbContext;

    public ValidTermChecker(EmsDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public bool IsValidTerm(long termId)
    {
      return _dbContext.ExamTerms.Any(x => x.Id == termId);
    }
  }
}