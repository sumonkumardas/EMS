using SinePulse.EMS.Persistence.Facade;
using System.Linq;

namespace SinePulse.EMS.UseCases.Repositories
{
  public class UniqueWorkingShiftPropertyChecker : IUniqueWorkingShiftPropertyChecker
  {
    private readonly EmsDbContext _dbContext;

    public UniqueWorkingShiftPropertyChecker(EmsDbContext dbContext)
    {
      _dbContext = dbContext;
    }
    public bool IsUniqueWorkingShiftName(string shiftName)
    {
      return !_dbContext.WorkingShifts.Any(s => s.ShiftName == shiftName); 
    }
  }
}