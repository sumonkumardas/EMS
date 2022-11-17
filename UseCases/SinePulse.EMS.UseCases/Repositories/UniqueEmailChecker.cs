using System.Linq;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Repositories
{
  public class UniqueEmailChecker : IUniqueEmailChecker
  {
    private readonly EmsDbContext _dbContext;

    public UniqueEmailChecker(EmsDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public bool IsUniqueEmail(string email)
    {
      return !_dbContext.Institutes.Any(b => b.Email == email) &&
             !_dbContext.Branches.Any(b => b.Email == email);
    }

    public bool IsUniqueInstitueEmail(string email,long id)
    {
      return !_dbContext.Institutes.Any(b => b.Email == email && b.Id != id) &&
             !_dbContext.Branches.Any(b => b.Email == email);
    }

    public bool IsUniqueBranchEmail(string email, long id)
    {
      return !_dbContext.Institutes.Any(b => b.Email == email ) &&
             !_dbContext.Branches.Any(b => b.Email == email && b.Id != id);
    }

    public bool IsUniqueContactPersonEmail(string email)
    {
      return !_dbContext.ContactPersons.Any(b => b.EmailAddress == email);
    }

    public bool IsUniquePhoneNo(string phoneNo)
    {
      return !_dbContext.ContactPersons.Any(b => b.PhoneNo == phoneNo);
    }
  }
}