using System;
using System.Linq;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Repositories
{
  public class LeaveAmountValidityChecker : ILeaveAmountValidityChecker
  {
    private readonly EmsDbContext _dbContext;

    public LeaveAmountValidityChecker(EmsDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public bool IsLeaveAmountValid(long employeeId,long leaveTypeId, DateTime startDate, DateTime endDate)
    {
      var currentLeaveType = _dbContext.LeaveTypes.FirstOrDefault(x=>x.Id == leaveTypeId);
      var preViousLeaves = _dbContext.EmployeeLeave.Where(x=>(x.LeaveStatus == Domain.Enums.LeaveStatusEnum.Pending )&& x.Employee.Id == employeeId && x.LeaveType.Id == leaveTypeId).ToList();
      int totalLeaves = 0;
      foreach (var leave in preViousLeaves)
      {
        totalLeaves += (leave.EndDate - leave.StartDate).Days;
      }
      var dayDifference = (endDate - startDate).Days;
      if (dayDifference == 0) dayDifference = 1;
      totalLeaves += dayDifference;
      return currentLeaveType.LeavesPerYear >= totalLeaves;
    }

    public bool IsMediumPreviouslyAdded(long mediumId)
    {
      return !_dbContext.BranchMediums.Any(s => s.Medium.Id == mediumId);
    }

    public bool IsMediumPreviouslyAdded(long mediumId, long branchMediumId)
    {
      return !_dbContext.BranchMediums.Any(se => se.Medium.Id == mediumId && se.Id != branchMediumId);
    }
  }
}