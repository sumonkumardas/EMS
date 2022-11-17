using FluentValidation.Results;
using SinePulse.EMS.Domain.Employees;
using SinePulse.EMS.Messages.Model.Employees;
using System.Collections.Generic;

namespace SinePulse.EMS.Messages.EmployeeGradeMessages
{
  public class ShowEmployeeGradeResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public GradeMessageModel EmployeeGrade { get; }

    public ShowEmployeeGradeResponseMessage(ValidationResult validationResult, GradeMessageModel employeeGrade)
    {
      ValidationResult = validationResult;
      EmployeeGrade = employeeGrade;
    }
  }
}