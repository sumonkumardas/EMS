using FluentValidation;
using SinePulse.EMS.Messages.EmployeeEducationalQualificationMessages;

namespace SinePulse.EMS.UseCases.EmployeeEducationalQualifications
{
  public class GetEmployeeEducationalQualificationsRequestMessageValidator : 
    AbstractValidator<GetEmployeeEducationalQualificationsRequestMessage>
  {
    public GetEmployeeEducationalQualificationsRequestMessageValidator()
    {
    }
  }
}