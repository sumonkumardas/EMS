using SinePulse.EMS.Messages.EmployeeGradeMessages;

namespace SinePulse.EMS.UseCases.Repositories
{
  public interface IUniqueEmployeeGradePropertyChecker
  {
    bool IsUniqueEmployeeGradeTitle(string gradeTitle);
    bool IsUniqueMinAndMaxSalary(AddEmployeeGradeRequestMessage employeeGrade);
    bool IsUniqueEmployeeGradeTitle(long gradeId,string gradeTitle);
    bool IsUniqueMinAndMaxSalary(EditEmployeeGradeRequestMessage employeeGrade);

  }
}