using FluentValidation.Results;
using SinePulse.EMS.Domain.Enums;
using System;

namespace SinePulse.EMS.Messages.EmployeeMessages
{
  public class AddEmployeeResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public long EmployeeId { get; }
    public AddEmployeeResponseMessage(ValidationResult validationResult, long employeeId)
    {
      ValidationResult = validationResult;
      EmployeeId = employeeId;
    }

    
  }
}