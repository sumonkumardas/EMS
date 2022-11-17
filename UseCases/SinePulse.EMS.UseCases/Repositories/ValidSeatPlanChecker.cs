using System.Linq;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Repositories
{
  public class ValidSeatPlanChecker : IValidSeatPlanChecker
  {
    private readonly EmsDbContext _dbContext;

    public ValidSeatPlanChecker(EmsDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public bool IsValidSeatPlan(long seatPlanId)
    {
      return _dbContext.SeatPlans.Any(x => x.Id == seatPlanId);
    }
  }
}