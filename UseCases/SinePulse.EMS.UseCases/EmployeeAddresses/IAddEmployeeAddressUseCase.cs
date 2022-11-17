using SinePulse.EMS.Domain.Employees;
using SinePulse.EMS.Messages.EmployeeAddressMessages;

namespace SinePulse.EMS.UseCases.EmployeeAddresses
{
  public interface IAddEmployeeAddressUseCase
  {
    void AddEmployeeAddress(AddEmployeeAddressRequestMessage message);
  }
}