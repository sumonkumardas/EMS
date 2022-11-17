using System.Linq;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Repositories
{
  public class ValidExamConfigurationChecker : IValidExamConfigurationChecker
  {
    private readonly EmsDbContext _dbContext;

    public ValidExamConfigurationChecker(EmsDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public bool IsValidExamConfiguration(long examConfigurationId)
    {
      return _dbContext.ExamConfigurations.Any(x => x.Id == examConfigurationId);
    }
  }
}