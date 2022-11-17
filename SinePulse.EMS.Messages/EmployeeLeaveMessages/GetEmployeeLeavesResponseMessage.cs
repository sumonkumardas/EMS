using System.Collections.Generic;
using FluentValidation.Results;
using SinePulse.EMS.Messages.Model.Leaves;

namespace SinePulse.EMS.Messages.EmployeeLeaveMessages
{
  public class GetEmployeeLeavesResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public IEnumerable<EmployeeLeaveMessageModel> EmployeeLeaves { get; }

    public GetEmployeeLeavesResponseMessage(ValidationResult validationResult,
      IEnumerable<EmployeeLeaveMessageModel> employeeLeaves)
    {
      ValidationResult = validationResult;
      EmployeeLeaves = employeeLeaves;
    }
  }
}