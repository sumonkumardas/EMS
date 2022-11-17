using System;
using System.Linq;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Repositories
{
  public class UniqueSessionPropertyChecker : IUniqueSessionPropertyChecker
  {
    private readonly EmsDbContext _dbContext;

    public UniqueSessionPropertyChecker(EmsDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public bool IsUniqueSessionName(long branchMediumId, string sessionName)
    {
      return !_dbContext.Sessions.Any(se => se.SessionName == sessionName && se.BranchMedium.Id == branchMediumId);
    }

    public bool IsSessionExists(long sessionId)
    {
      return _dbContext.Sessions.Any(s => s.Id == sessionId);
    }
    
    public bool IsSessionExists(DateTime sessionStartDate, DateTime sessionEndDate, long sessionId)
    {
      return _dbContext.Sessions.Any(s =>
        s.StartDate == sessionStartDate && s.EndDate == sessionEndDate && s.Id != sessionId);
    }

    public bool IsUniqueSessionName(string sessionName, long sessionId)
    {
      return !_dbContext.Sessions.Any(se => se.SessionName == sessionName && se.Id != sessionId);
    }
  }
}