using System.Linq;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Repositories
{
  public class UniqueTermPropertyChecker : IUniqueTermPropertyChecker
  {
    private readonly EmsDbContext _dbContext;

    public UniqueTermPropertyChecker(EmsDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public bool IsUniqueTermName(string termName, long sessionId,long branchMediumId)
    {
      return !_dbContext.ExamTerms.Where(t => t.Session.Id == sessionId && t.Session.BranchMediumId == branchMediumId).Any(t => t.TermName == termName);
    }
  }
}