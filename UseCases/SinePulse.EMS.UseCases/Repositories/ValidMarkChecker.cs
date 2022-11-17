using System.Linq;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Repositories
{
  public class ValidMarkChecker : IValidMarkChecker
  {
    private readonly EmsDbContext _dbContext;

    public ValidMarkChecker(EmsDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public bool IsValidMark(long markId)
    {
      return _dbContext.Marks.Any(x => x.Id == markId);
    }
  }
}