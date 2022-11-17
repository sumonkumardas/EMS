using System.Linq;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Repositories
{
  public class UniqueClassTestPropertyChecker : IUniqueClassTestPropertyChecker
  {
    private readonly EmsDbContext _dbContext;

    public UniqueClassTestPropertyChecker(EmsDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public bool IsUniqueClassTestName(string testName, long sectionId, long examConfigurationId)
    {
      return !_dbContext.ClassTests
        .Where(t => t.Section.Id == sectionId && t.ExamConfiguration.Id == examConfigurationId) 
        .Any(t => t.TestName == testName);
    }

    public bool IsUniqueClassTestName(string testName, long sectionId, long examConfigurationId,long classTestId)
    {
      return !_dbContext.ClassTests
        .Where(t => t.Section.Id == sectionId && t.ExamConfiguration.Id == examConfigurationId && t.Id!=classTestId)
        .Any(t => t.TestName == testName);
    }
  }
}