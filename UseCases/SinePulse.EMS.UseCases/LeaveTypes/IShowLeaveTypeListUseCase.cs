using SinePulse.EMS.Messages.LeaveTypeMessages;
using SinePulse.EMS.Messages.Model.Leaves;
using System.Collections.Generic;

namespace SinePulse.EMS.UseCases.LeaveTypes
{
  public interface IShowLeaveTypeListUseCase
  {
    List<LeaveTypeMessageModel> ShowLeaveTypeList(ShowLeaveTypeListRequestMessage message);
  }
}