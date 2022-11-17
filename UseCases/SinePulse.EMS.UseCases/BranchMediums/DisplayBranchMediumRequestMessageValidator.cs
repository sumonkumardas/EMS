using FluentValidation;
using SinePulse.EMS.Messages.BranchMediumMessages;

namespace SinePulse.EMS.UseCases.BranchMediums
{
  public class DisplayBranchMediumRequestMessageValidator : AbstractValidator<DisplayBranchMediumRequestMessage>
  {
    public DisplayBranchMediumRequestMessageValidator()
    {
      RuleFor(x => x.BranchMediumId).NotEmpty().WithMessage("Invalid Id.");
      RuleFor(x => x.BranchMediumId).GreaterThan(0).WithMessage("Invalid Id.");
    }
  }
}