using System.Linq;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Utility;

namespace SinePulse.EMS.UseCases.Repositories
{
  public class GradeMarksOverlappingChecker : IGradeMarksOverlappingChecker
  {
    private readonly EmsDbContext _dbContext;
    private readonly IIntegerOverlappingChecker _integerOverlappingChecker;

    public GradeMarksOverlappingChecker(EmsDbContext dbContext,
      IIntegerOverlappingChecker integerOverlappingChecker)
    {
      _dbContext = dbContext;
      _integerOverlappingChecker = integerOverlappingChecker;
    }

    public bool IsNonOverlappingGradeMarks(decimal gradePoint,int startMark, int endMark, long branchMediumId)
    {
      return !_dbContext.ResultGrades
        .Where(x => x.BranchMedium.Id == branchMediumId)
        .Any(t => t.GradePoint == gradePoint || (t.StartMark == startMark || t.EndMark == endMark || (startMark>t.StartMark && startMark<=t.EndMark)|| (endMark >= t.StartMark && endMark < t.EndMark)) );
    }

    public bool IsNonOverlappingGradeMarks(int startMark, int endMark, long branchMediumId, long resultGradeId)
    {
      return !_dbContext.ResultGrades
        .Where(x => x.BranchMedium.Id == branchMediumId && x.Id != resultGradeId)
        .Any(t => (t.StartMark == startMark || t.EndMark == endMark || (startMark > t.StartMark && startMark <= t.EndMark) || (endMark >= t.StartMark && endMark < t.EndMark)));
    }

    private bool DoesOverlap(int startMark1, int endMark1, int startMark2, int endMark2)
    {
      return _integerOverlappingChecker.DoesOverlap(startMark1, endMark1, startMark2, endMark2);
    }
  }
}