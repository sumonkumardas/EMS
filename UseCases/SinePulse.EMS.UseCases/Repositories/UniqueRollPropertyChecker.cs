using System.Linq;
using Microsoft.AspNetCore.Identity;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Repositories
{
  public class UniqueRollPropertyChecker : IUniqueRollPropertyChecker
  {
    private readonly EmsDbContext _dbContext;

    public UniqueRollPropertyChecker(EmsDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public bool IsUniqueRollNo(int rollNo, long sectionId, long classId)
    {
      return !_dbContext.StudentSections.Where(r => r.Section.Id == sectionId && r.Class.Id == classId).Any(r => r.RollNo == rollNo);
    }

    public bool IsUniqueRollNo(int rollNo, long sectionId, long classId, long studentId)
    {
      return !_dbContext.StudentSections.Where(r => r.Section.Id == sectionId && r.Class.Id == classId && r.Student.Id !=studentId).Any(r => r.RollNo == rollNo);
    }
  }
}