using System.Linq;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Repositories
{
  public class ValidResultGradeChecker : IValidResultGradeChecker
  {
    private readonly EmsDbContext _dbContext;

    public ValidResultGradeChecker(EmsDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public bool IsValidResultGrade(long resultGradeId)
    {
      return _dbContext.ResultGrades.Any(x => x.Id == resultGradeId);
    }
  }
}