using SinePulse.EMS.Messages.EmployeeLeaveMessages;
using SinePulse.EMS.Messages.Model.Leaves;
using System.Collections.Generic;

namespace SinePulse.EMS.UseCases.EmployeeLeaves
{
  public interface IShowEmployeeLeaveListUseCase
  {
    List<EmployeeLeaveMessageModel> ShowUnaprrovedLeaveList(ShowEmployeeLeaveListRequestMessage request);
  }
}