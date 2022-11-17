using System.Linq;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Repositories
{
  public class UniqueClassPropertyChecker : IUniqueClassPropertyChecker
  {
    private readonly EmsDbContext _dbContext;

    public UniqueClassPropertyChecker(EmsDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public bool IsUniqueClassName(string className)
    {
      return !_dbContext.Classes.Any(c => c.ClassName == className);
    }

    public bool IsUniqueClassCode(int classCode)
    {
      return !_dbContext.Classes.Any(c => c.ClassCode == classCode);
    }

    public bool IsUniqueClassCode(int classCode, long classId)
    {
      return !_dbContext.Classes.Any(c => c.ClassCode == classCode && c.Id != classId);
    }

    public bool IsUniqueClassName(string className, long classId)
    {
      return !_dbContext.Classes.Any(c => c.ClassName == className && c.Id != classId);
    }
  }
}