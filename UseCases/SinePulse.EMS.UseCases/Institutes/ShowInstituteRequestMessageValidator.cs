using FluentValidation;
using SinePulse.EMS.Messages.InstituteMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.Institutes
{
  public class ShowInstituteRequestMessageValidator : AbstractValidator<ShowInstituteRequestMessage>
  {

    public ShowInstituteRequestMessageValidator()
    {
      RuleFor(x => x.InstituteId).NotEmpty().WithMessage("Invalid Id.");
      RuleFor(x => x.InstituteId).GreaterThan(0).WithMessage("Invalid Id.");
    }
    
  }
}