using FluentValidation;
using SinePulse.EMS.Messages.EmployeePersonalInfoMessages;

namespace SinePulse.EMS.UseCases.EmployeePersonalInfo
{
  public class GetEmployeePersonalInfoRequestMessageValidator : AbstractValidator<GetEmployeePersonalInfoRequestMessage>
  {
    public GetEmployeePersonalInfoRequestMessageValidator()
    {
    }
  }
}