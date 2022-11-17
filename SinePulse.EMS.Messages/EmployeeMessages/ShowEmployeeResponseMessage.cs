using FluentValidation.Results;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Messages.Model.Employees;
using System.Collections.Generic;

namespace SinePulse.EMS.Messages.EmployeeMessages
{
  public class ShowEmployeeResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public EmployeeMessageModel Employee { get; }

    public ShowEmployeeResponseMessage(ValidationResult validationResult, EmployeeMessageModel employee)
    {
      ValidationResult = validationResult;
      Employee = employee;
    }
  }
}