using FluentValidation.Results;

namespace SinePulse.EMS.Messages.BranchMediumMessages
{
  public class AddBranchMediumResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public long BranchMediumId { get; }
    public AddBranchMediumResponseMessage(ValidationResult validationResult,long branchMediumId)
    {
      ValidationResult = validationResult;
      BranchMediumId = branchMediumId;
    }
  }
}