using FluentValidation;
using SinePulse.EMS.Messages.SectionMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.Sections
{
  public class ShowSectionRequestMessageValidator : AbstractValidator<ShowSectionRequestMessage>
  {

    public ShowSectionRequestMessageValidator()
    {
      RuleFor(x => x.SectionId).NotEmpty().WithMessage("Invalid Id.");
      RuleFor(x => x.SectionId).GreaterThan(0).WithMessage("Invalid Id.");
    }
  }
}