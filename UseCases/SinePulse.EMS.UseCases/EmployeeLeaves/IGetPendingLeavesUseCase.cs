using System.Collections.Generic;
using SinePulse.EMS.Messages.EmployeeLeaveMessages;

namespace SinePulse.EMS.UseCases.EmployeeLeaves
{
  public interface IGetPendingLeavesUseCase
  {
    IEnumerable<GetPendingLeavesResponseMessage.PendingLeave> GetPendingLeaves(GetPendingLeavesRequestMessage message);
  }
}