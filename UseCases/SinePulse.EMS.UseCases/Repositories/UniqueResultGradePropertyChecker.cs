using System.Linq;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Repositories
{
  public class UniqueResultGradePropertyChecker : IUniqueResultGradePropertyChecker
  {
    private readonly EmsDbContext _dbContext;

    public UniqueResultGradePropertyChecker(EmsDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public bool IsUniqueResultGradeLetter(string gradeLetter, long branchMediumId)
    {
      return !_dbContext.ResultGrades
        .Where(x => x.BranchMedium.Id == branchMediumId)
        .Any(x => x.GradeLetter == gradeLetter);
    }

    public bool IsUniqueResultGradePoint(decimal gradePoint, long branchMediumId)
    {
      return !_dbContext.ResultGrades
        .Where(x => x.BranchMedium.Id == branchMediumId)
        .Any(x => x.GradePoint == gradePoint);
    }

    public bool IsUniqueResultGradeLetter(string gradeLetter, long branchMediumId,long resultGradeId)
    {
      return !_dbContext.ResultGrades
        .Where(x => x.BranchMedium.Id == branchMediumId)
        .Any(x => x.GradeLetter == gradeLetter && x.Id!=resultGradeId);
    }

    public bool IsUniqueResultGradePoint(decimal gradePoint, long branchMediumId, long resultGradeId)
    {
      return !_dbContext.ResultGrades
        .Where(x => x.BranchMedium.Id == branchMediumId)
        .Any(x => x.GradePoint == gradePoint && x.Id != resultGradeId);
    }
  }
}