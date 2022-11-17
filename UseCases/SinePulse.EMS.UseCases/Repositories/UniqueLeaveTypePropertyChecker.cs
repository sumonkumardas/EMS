using System.Linq;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Repositories
{
  public class UniqueLeaveTypePropertyChecker : IUniqueLeaveTypePropertyChecker
  {
    private readonly EmsDbContext _dbContext;

    public UniqueLeaveTypePropertyChecker(EmsDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public bool IsUniqueLeaveName(string leaveName)
    {
      return !_dbContext.LeaveTypes.Any(l => l.LeaveName == leaveName);
    }

    public bool IsUniqueLeaveName(string leaveName,long leaveId)
    {
      return !_dbContext.LeaveTypes.Any(l => l.LeaveName == leaveName && l.Id != leaveId);
    }
  }
}