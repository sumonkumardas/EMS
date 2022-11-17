using SinePulse.EMS.Messages.EmployeeAddressMessages;

namespace SinePulse.EMS.UseCases.Employee
{
  public interface IGetEmployeeAddressUseCase
  {
    GetEmployeeAddressResponseMessage.EmployeeAddress GetEmployeeAddress(GetEmployeeAddressRequestMessage message);
  }
}