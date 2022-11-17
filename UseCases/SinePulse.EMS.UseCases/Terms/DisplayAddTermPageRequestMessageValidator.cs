using FluentValidation;
using SinePulse.EMS.Messages.TermMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.Terms
{
  public class DisplayAddTermPageRequestMessageValidator : AbstractValidator<DisplayAddTermPageRequestMessage>
  {
    private readonly IValidBranchMediumChecker _validBranchMediumChecker;

    public DisplayAddTermPageRequestMessageValidator(IValidBranchMediumChecker validBranchMediumChecker)
    {
      _validBranchMediumChecker = validBranchMediumChecker;
      RuleFor(x => x.BranchMediumId).Must(IsValidBranchMedium).WithMessage("Invalid Medium");
    }

    private bool IsValidBranchMedium(long branchMediumId)
    {
      return _validBranchMediumChecker.IsValidBranchMedium(branchMediumId);
    }
  }
}