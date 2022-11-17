using FluentValidation;
using SinePulse.EMS.Messages.EmployeeGradeMessages;

namespace SinePulse.EMS.UseCases.EmployeeGrade
{
  public class ShowEmployeeGradeRequestMessageValidator : AbstractValidator<ShowEmployeeGradeRequestMessage>
  {

    public ShowEmployeeGradeRequestMessageValidator()
    {
      RuleFor(x => x.GradeId).NotEmpty().WithMessage("Invalid Id.");
      RuleFor(x => x.GradeId).GreaterThan(0).WithMessage("Invalid Id.");
    }
    
  }
}