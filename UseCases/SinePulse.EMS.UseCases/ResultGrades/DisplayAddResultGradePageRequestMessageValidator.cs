using FluentValidation;
using SinePulse.EMS.Messages.ResultGradeMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.ResultGrades
{
  public class DisplayAddResultGradePageRequestMessageValidator 
    : AbstractValidator<DisplayAddResultGradePageRequestMessage>
  {
    private readonly IValidBranchMediumChecker _validBranchMediumChecker;

    public DisplayAddResultGradePageRequestMessageValidator(IValidBranchMediumChecker validBranchMediumChecker)
    {
      _validBranchMediumChecker = validBranchMediumChecker;
      RuleFor(x => x.BranchMediumId).Must(IsValidBranchMedium).WithMessage("Invalid Branch Medium");
    }

    private bool IsValidBranchMedium(long termId)
    {
      return _validBranchMediumChecker.IsValidBranchMedium(termId);
    }
  }
}