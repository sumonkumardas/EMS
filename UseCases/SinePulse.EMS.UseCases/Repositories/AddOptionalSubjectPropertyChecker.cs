using System.Linq;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Repositories
{
  public class AddOptionalSubjectPropertyChecker : IAddOptionalSubjectPropertyChecker
  {
    private readonly EmsDbContext _dbContext;

    public AddOptionalSubjectPropertyChecker(EmsDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public bool IsOptionalSubjectAlreadyAdded(long studentId, long subjectId, long classId, GroupEnum group)
    {
      return !_dbContext.SubjectChoices.Any(s => s.Student.Id == studentId &&
                                                 s.Subject.Subject.Id == subjectId &&
                                                 s.Subject.Class.Id == classId &&
                                                 s.Subject.Group == group);
    }
  }
}