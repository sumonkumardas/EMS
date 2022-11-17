using FluentValidation;
using SinePulse.EMS.Messages.EmployeeGradeMessages;

namespace SinePulse.EMS.UseCases.EmployeeGrade
{
  public class ShowEmployeeGradeListRequestMessageValidator : AbstractValidator<ShowEmployeeGradeListRequestMessage>
  {

    public ShowEmployeeGradeListRequestMessageValidator()
    {
    }
    
  }
}