using System.Linq;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Repositories
{
  public class ShiftExistanceChecker : IShiftExistanceChecker
  {
    private readonly EmsDbContext _dbContext;

    public ShiftExistanceChecker(EmsDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public bool IsShiftExists(long shiftId)
    {
      return _dbContext.Shifts.Any(s => s.Id == shiftId);
    }
  }
}