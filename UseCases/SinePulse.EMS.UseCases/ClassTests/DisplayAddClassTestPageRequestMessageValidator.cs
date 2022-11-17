using FluentValidation;
using SinePulse.EMS.Messages.ClassTestMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.ClassTests
{
  public class DisplayAddClassTestPageRequestMessageValidator 
    : AbstractValidator<DisplayAddClassTestPageRequestMessage>
  {
    private readonly IValidSectionChecker _validSectionChecker;

    public DisplayAddClassTestPageRequestMessageValidator(IValidSectionChecker validSectionChecker)
    {
      _validSectionChecker = validSectionChecker;
      RuleFor(x => x.SectionId).Must(IsValidSection).WithMessage("Invalid Section");
    }

    private bool IsValidSection(long sectionId)
    {
      return _validSectionChecker.IsValidSection(sectionId);
    }
  }
}