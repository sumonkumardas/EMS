using System.Linq;
using SinePulse.EMS.Messages.EmployeeGradeMessages;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Repositories
{
  public class UniqueEmployeeGradePropertyChecker : IUniqueEmployeeGradePropertyChecker
  {
    private readonly EmsDbContext _dbContext;

    public UniqueEmployeeGradePropertyChecker(EmsDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public bool IsUniqueEmployeeGradeTitle(string gradeTitle)
    {
      return !_dbContext.Grades.Any(g => g.GradeTitle == gradeTitle);
    }

    public bool IsUniqueEmployeeGradeTitle(long gradeId, string gradeTitle)
    {
      return !_dbContext.Grades.Any(g => g.GradeTitle == gradeTitle && g.Id != gradeId);
    }

    public bool IsUniqueMinAndMaxSalary(AddEmployeeGradeRequestMessage employeeGrade)
    {
      return !_dbContext.Grades.Any(g => g.MaxSalary == employeeGrade.MaxSalary && g.MinSalary == employeeGrade.MinSalary);
    }

    public bool IsUniqueMinAndMaxSalary(EditEmployeeGradeRequestMessage employeeGrade)
    {
      return !_dbContext.Grades.Any(g => g.MaxSalary == employeeGrade.MaxSalary && g.MinSalary == employeeGrade.MinSalary && g.Id != employeeGrade.Id);
    }
  }
}