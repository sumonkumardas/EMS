using System.Collections.Generic;
using SinePulse.EMS.Messages.EmployeeLeaveMessages;
using SinePulse.EMS.Messages.Model.Leaves;

namespace SinePulse.EMS.UseCases.EmployeeLeaves
{
  public interface IGetEmployeeLeavesUseCase
  {
    List<EmployeeLeaveMessageModel> GetEmployeeLeaves(GetEmployeeLeavesRequestMessage message);
  }
}