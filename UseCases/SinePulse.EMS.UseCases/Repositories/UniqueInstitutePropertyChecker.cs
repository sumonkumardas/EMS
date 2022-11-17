using System.Linq;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Repositories
{
  public class UniqueInstitutePropertyChecker : IUniqueInstitutePropertyChecker
  {
    private readonly EmsDbContext _dbContext;

    public UniqueInstitutePropertyChecker(EmsDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public bool IsUniqueInstituteName(string instituteName)
    {
      return !_dbContext.Institutes.Any(s => s.OrganisationName == instituteName);
    }

    public bool IsUniqueInstituteShortName(string instituteShortName)
    {
      return !_dbContext.Institutes.Any(s => s.ShortName == instituteShortName);
    }

    public bool IsUniqueInstituteName(string organizationName, long instituteId)
    {
      return !_dbContext.Institutes.Any(se => se.OrganisationName == organizationName && se.Id != instituteId);
    }

    public bool IsUniqueInstituteShortName(string shortName, long instituteId)
    {
      return !_dbContext.Institutes.Any(se => se.ShortName == shortName && se.Id != instituteId);
    }
  }
}