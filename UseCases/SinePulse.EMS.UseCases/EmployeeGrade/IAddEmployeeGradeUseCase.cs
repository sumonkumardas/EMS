using SinePulse.EMS.Domain.Employees;
using SinePulse.EMS.Messages.EmployeeGradeMessages;

namespace SinePulse.EMS.UseCases.EmployeeGrade
{
  public interface IAddEmployeeGradeUseCase
  {
    Grade AddEmployeeGrade(AddEmployeeGradeRequestMessage message);
  }
}