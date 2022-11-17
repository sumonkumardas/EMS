using System.Linq;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Repositories
{
  public class ValidStudentChecker : IValidStudentChecker
  {
    private readonly EmsDbContext _dbContext;

    public ValidStudentChecker(EmsDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public bool IsValidStudent(long studentId)
    {
      return _dbContext.Students.Any(x => x.Id == studentId);
    }
  }
}