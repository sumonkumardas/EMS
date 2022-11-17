using FluentValidation;
using SinePulse.EMS.Messages.ImportSessionDataMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.ImportSessionDatas
{
  public class DisplayImportSessionDataPageRequestMessageValidator : AbstractValidator<DisplayImportSessionDataPageRequestMessage>
  {
    private readonly IValidBranchMediumChecker _validBranchMediumChecker;

    public DisplayImportSessionDataPageRequestMessageValidator(IValidBranchMediumChecker validBranchMediumChecker)
    {
      _validBranchMediumChecker = validBranchMediumChecker;
      RuleFor(x => x.BranchMediumId).Must(IsValidBranchMedium).WithMessage("Invalid Branch Medium");
    }

    private bool IsValidBranchMedium(long testId)
    {
      return _validBranchMediumChecker.IsValidBranchMedium(testId);
    }
  }
}