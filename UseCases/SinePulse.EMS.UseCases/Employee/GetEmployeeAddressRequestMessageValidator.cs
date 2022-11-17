using FluentValidation;
using SinePulse.EMS.Messages.EmployeeAddressMessages;

namespace SinePulse.EMS.UseCases.Employee
{
  public class GetEmployeeAddressRequestMessageValidator : AbstractValidator<GetEmployeeAddressRequestMessage>
  {
    public GetEmployeeAddressRequestMessageValidator()
    {
    }
  }
}