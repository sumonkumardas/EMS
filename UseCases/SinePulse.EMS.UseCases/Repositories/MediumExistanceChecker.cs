using System.Linq;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Repositories
{
  public class MediumExistanceChecker : IMediumExistanceChecker
  {
    private readonly EmsDbContext _dbContext;

    public MediumExistanceChecker(EmsDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public bool IsMediumExists(long mediumId)
    {
      return _dbContext.Mediums.Any(m => m.Id == mediumId);
    }
  }
}