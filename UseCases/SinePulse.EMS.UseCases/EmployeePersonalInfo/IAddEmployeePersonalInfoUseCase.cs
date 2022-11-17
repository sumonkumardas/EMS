using SinePulse.EMS.Messages.EmployeePersonalInfoMessages;

namespace SinePulse.EMS.UseCases.EmployeePersonalInfo
{
  public interface IAddEmployeePersonalInfoUseCase
  {
    void AddEmployeePersonalInfo(AddEmployeePersonalInfoRequestMessage request);
  }
}