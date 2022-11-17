using System.Linq;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Messages.ExamConfigurationMessages;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Repositories
{
  public class UniqueExamConfigurationPropertyChecker : IUniqueExamConfigurationPropertyChecker
  {
    private readonly EmsDbContext _dbContext;

    public UniqueExamConfigurationPropertyChecker(EmsDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public bool IsUniqueExamConfigurationMark(long classId, long groupId, long subjectId,long termId)
    {
        return !_dbContext.ExamConfigurations
          .Any(x => x.Class.Id == classId && x.Subject.Id == subjectId && x.Term.Id == termId);
    }

    public bool IsUniqueExamConfigurationMark(long classId, long groupId, long subjectId, long termId,long examConfigurationId)
    {
      return !_dbContext.ExamConfigurations
        .Any(x => x.Class.Id == classId && x.Subject.Id == subjectId && x.Term.Id == termId & x.Id!=examConfigurationId);
    }

    public bool IsMarksAlreadyAdded(long examConfigurationId)
    {
      return !_dbContext.TermTestMarks
        .Any(x => x.TermTest.ExamConfiguration.Id == examConfigurationId);
    }
  }
}