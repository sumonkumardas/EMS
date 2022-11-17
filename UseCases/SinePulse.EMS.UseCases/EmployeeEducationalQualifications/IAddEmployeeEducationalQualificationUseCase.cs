using SinePulse.EMS.Messages.EmployeeEducationalQualificationMessages;

namespace SinePulse.EMS.UseCases.EmployeeEducationalQualifications
{
  public interface IAddEmployeeEducationalQualificationUseCase
  {
    void AddEmployeeEducationalQualification(AddEmployeeEducationalQualificationRequestMessage request);
  }
}