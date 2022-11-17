using SinePulse.EMS.Messages.EmployeeAddressMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Employee
{
  public class GetEmployeeAddressUseCase : IGetEmployeeAddressUseCase
  {
    private readonly IRepository _repository;

    public GetEmployeeAddressUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public GetEmployeeAddressResponseMessage.EmployeeAddress GetEmployeeAddress(
      GetEmployeeAddressRequestMessage message)
    {
      var employee = _repository.GetByIdWithInclude<Domain.Employees.Employee>(message.EmployeeId,
        new[] {nameof(Domain.Employees.Employee.PresentAddress), nameof(Domain.Employees.Employee.PermanentAddress)});

      if (employee.PermanentAddress != null && employee.PresentAddress != null)
      {
        return new GetEmployeeAddressResponseMessage.EmployeeAddress
        {
          PresentStreet1 = employee.PresentAddress.Street1,
          PresentStreet2 = employee.PresentAddress.Street2,
          PresentPostalCode = employee.PresentAddress.PostalCode,
          PresentCity = employee.PresentAddress.City,
          PermanentStreet1 = employee.PermanentAddress.Street1,
          PermanentStreet2 = employee.PermanentAddress.Street2,
          PermanentPostalCode = employee.PermanentAddress.PostalCode,
          PermanentCity = employee.PermanentAddress.City
        };
      }

      return new GetEmployeeAddressResponseMessage.EmployeeAddress();
    }
  }
}