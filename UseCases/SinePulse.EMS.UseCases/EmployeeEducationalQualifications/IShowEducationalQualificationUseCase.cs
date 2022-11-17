using SinePulse.EMS.Messages.EmployeeEducationalQualificationMessages;
using SinePulse.EMS.Messages.Model.Employees;

namespace SinePulse.EMS.UseCases.EmployeeEducationalQualifications
{
  public interface IShowEducationalQualificationUseCase
  {
    EducationalQualificationMessageModel ShowEducationalQualification(ShowEducationalQualificationRequestMessage message);
  }
}