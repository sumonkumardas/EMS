
using SinePulse.EMS.Messages.EmployeeGradeMessages;
using System.Collections.Generic;
using SinePulse.EMS.Messages.Model.Employees;

namespace SinePulse.EMS.UseCases.EmployeeGrade
{
  public interface IShowEmployeeGradeListUseCase
  {
    List<GradeMessageModel> ShowEmployeeGradeList(ShowEmployeeGradeListRequestMessage request);
  }
}