using System.Linq;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Repositories
{
  public class UniqueCommitteeMemberPropertyChecker : IUniqueCommitteeMemberPropertyChecker
  {
    private readonly EmsDbContext _dbContext;

    public UniqueCommitteeMemberPropertyChecker(EmsDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public bool IsUniqueMobileNo(string mobileNo)
    {
      return !_dbContext.CommitteeMembers.Any(c => c.MobileNo == mobileNo);      
    }

    public bool IsUniqueEmailAddress(string emailAddress)
    {
      return !_dbContext.CommitteeMembers.Any(c => c.EmailAddress == emailAddress); 
    }
  }
}