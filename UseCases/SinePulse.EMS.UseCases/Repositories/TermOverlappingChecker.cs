using System;
using System.Linq;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Repositories
{
  public class TermOverlappingChecker : ITermOverlappingChecker
  {
    private readonly EmsDbContext _dbContext;

    public TermOverlappingChecker(EmsDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public bool IsUniqueTermName(string termName, long sessionId)
    {
      return !_dbContext.ExamTerms.Where(t => t.Session.Id == sessionId).Any(t => t.TermName == termName);
    }

    public bool IsNonOverlappingWithSessionStartTime(DateTime startDate, long sessionId, long branchMediumId)
    {
      var session = _dbContext.Sessions.FirstOrDefault(x => x.Id == sessionId);

      if (session == null)
        return true;
      return startDate.DayOfYear >= session.StartDate.DayOfYear &&
             startDate.DayOfYear < session.EndDate.DayOfYear;
    }

    public bool IsNonOverlappingWithSessionEndTime(DateTime endDate, long sessionId, long branchMediumId)
    {
      var session = _dbContext.Sessions.FirstOrDefault(x => x.Id == sessionId);

      if (session == null)
        return true;
      return endDate.DayOfYear > session.StartDate.DayOfYear &&
             endDate.DayOfYear <= session.EndDate.DayOfYear;
    } 

    public bool IsNonOverlappingWithTermStartTime(DateTime startDate, long sessionId, long branchMediumId)
    {
      var terms = _dbContext.ExamTerms.Where(x => x.Session.Id == sessionId && x.Session.BranchMediumId == branchMediumId);

      foreach (var term in terms)
        return startDate.DayOfYear >= term.StartDate.DayOfYear &&
               startDate.DayOfYear < term.EndDate.DayOfYear;

      return false;

    }

    public bool IsNonOverlappingWithTermEndTime(DateTime endDate, long sessionId, long branchMediumId)
    {
      var terms = _dbContext.ExamTerms.Where(x => x.Session.Id == sessionId && x.Session.BranchMediumId == branchMediumId);

      foreach (var term in terms)
        return endDate.DayOfYear > term.StartDate.DayOfYear &&
             endDate.DayOfYear <= term.EndDate.DayOfYear;

      return false;
    }
  }
}