using SinePulse.EMS.Messages.EmployeePersonalInfoMessages;

namespace SinePulse.EMS.UseCases.EmployeePersonalInfo
{
  public interface IGetEmployeePersonalInfoUseCase
  {
    GetEmployeePersonalInfoResponseMessage.EmployeePersonalInfo GetEmployeePersonalInfo(
      GetEmployeePersonalInfoRequestMessage message);
  }
}