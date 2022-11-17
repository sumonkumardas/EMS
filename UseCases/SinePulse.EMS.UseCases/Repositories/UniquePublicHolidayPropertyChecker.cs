using System.Linq;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Repositories
{
  public class UniquePublicHolidayPropertyChecker : IUniquePublicHolidayPropertyChecker
  {
    private readonly EmsDbContext _dbContext;

    public UniquePublicHolidayPropertyChecker(EmsDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public bool IsUniquePublicHolidayName(string holidayName, int year)
    {
      return !_dbContext.PublicHolidays
        .Where(h => h.StartDate.Year == year)
        .Any(r => r.HolidayName == holidayName);
    }

    public bool IsUniquePublicHolidayName(string holidayName, int year,long publicHolidayId)
    {
      return !_dbContext.PublicHolidays
        .Where(h => h.StartDate.Year == year && h.Id != publicHolidayId)
        .Any(r => r.HolidayName == holidayName);
    }
  }
}