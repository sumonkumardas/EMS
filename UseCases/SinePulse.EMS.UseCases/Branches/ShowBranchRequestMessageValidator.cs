using FluentValidation;
using SinePulse.EMS.Messages.BranchMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.Branches
{
  public class ShowBranchRequestMessageValidator : AbstractValidator<ShowBranchRequestMessage>
  {

    public ShowBranchRequestMessageValidator()
    {
      RuleFor(x => x.BranchId).NotEmpty().WithMessage("Invalid Id.");
      RuleFor(x => x.BranchId).GreaterThan(0).WithMessage("Invalid Id.");
    }
    
  }
}