using FluentValidation;
using SinePulse.EMS.Messages.EmployeeEducationalQualificationMessages;

namespace SinePulse.EMS.UseCases.EmployeeEducationalQualifications
{
  public class
    ShowEducationalQualificationRequestMessageValidator : AbstractValidator<ShowEducationalQualificationRequestMessage>
  {
    public ShowEducationalQualificationRequestMessageValidator()
    {
    }
  }
}