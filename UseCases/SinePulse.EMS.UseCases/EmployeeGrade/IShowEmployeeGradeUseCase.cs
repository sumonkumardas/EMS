
using SinePulse.EMS.Messages.EmployeeGradeMessages;
using System.Collections.Generic;
using SinePulse.EMS.Messages.Model.Employees;

namespace SinePulse.EMS.UseCases.EmployeeGrade
{
  public interface IShowEmployeeGradeUseCase
  {
    GradeMessageModel ShowEmployeeGrade(ShowEmployeeGradeRequestMessage request);
  }
}