using FluentValidation.Results;
using SinePulse.EMS.Messages.Model.Employees;
using System.Collections.Generic;

namespace SinePulse.EMS.Messages.EmployeeGradeMessages
{
  public class ShowEmployeeGradeListResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public List<GradeMessageModel> EmployeeGradeList { get; }

    public ShowEmployeeGradeListResponseMessage(ValidationResult validationResult, List<GradeMessageModel> employeeGradeList)
    {
      ValidationResult = validationResult;
      EmployeeGradeList = employeeGradeList;
    }
  }
}