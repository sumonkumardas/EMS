using System.Linq;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Repositories
{
  public class UniqueSubjectPropertyChecker : IUniqueSubjectPropertyChecker
  {
    private readonly EmsDbContext _dbContext;

    public UniqueSubjectPropertyChecker(EmsDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public bool IsUniqueSubjectName(string subjectName)
    {
      return !_dbContext.Subjects.Any(s => s.SubjectName == subjectName);
    }

    public bool IsUniqueSubjectCode(int subjectCode)
    {
      return !_dbContext.Subjects.Any(s => s.SubjectCode == subjectCode);
    }
    
    public bool IsUniqueSubjectCode(int subjectCode, long subjectId)
    {
      return !_dbContext.Subjects.Any(s => s.SubjectCode == subjectCode && s.Id != subjectId);
    }

    public bool IsUniqueSubjectName(string subjectName, long subjectId)
    {
      return !_dbContext.Subjects.Any(s => s.SubjectName == subjectName && s.Id != subjectId);
    }
  }
}