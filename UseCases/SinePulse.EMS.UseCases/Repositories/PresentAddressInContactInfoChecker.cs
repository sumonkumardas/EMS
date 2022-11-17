using System.Linq;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Repositories
{
  public class PresentAddressInContactInfoChecker : IPresentAddressInContactInfoChecker
  {
    private readonly EmsDbContext _dbContext;

    public PresentAddressInContactInfoChecker(EmsDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public bool IsPresentAddressAdded(long contactInfoId)
    {
      return _dbContext.ContactInfos.Any(c => c.Id == contactInfoId && c.PresentAddress != null);
    }
  }
}