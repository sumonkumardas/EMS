using SinePulse.EMS.Messages.EmployeeEducationalQualificationMessages;

namespace SinePulse.EMS.UseCases.EmployeeEducationalQualifications
{
  public interface IEditEducationalQualificationUseCase
  {
    long EditEducationalQualification(EditEducationalQualificationRequestMessage message);
  }
}