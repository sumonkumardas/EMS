using System;

namespace SinePulse.EMS.UseCases.Repositories
{
  public interface ILeaveAmountValidityChecker
  {
    bool IsLeaveAmountValid(long employeeId,long leaveTypeId, DateTime startDate, DateTime endDate);
  }
}