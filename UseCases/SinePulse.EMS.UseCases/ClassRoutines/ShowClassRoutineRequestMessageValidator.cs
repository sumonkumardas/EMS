using FluentValidation;
using SinePulse.EMS.Messages.ClassRoutineMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.ClassRoutines
{
  public class ShowClassRoutineRequestMessageValidator : AbstractValidator<ShowClassRoutineRequestMessage>
  {

    public ShowClassRoutineRequestMessageValidator()
    {
      RuleFor(x => x.ClassRoutineId).NotEmpty().WithMessage("Invalid Id.");
      RuleFor(x => x.ClassRoutineId).GreaterThan(0).WithMessage("Invalid Id.");
    }
    
  }
}