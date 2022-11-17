using FluentValidation.Results;

namespace SinePulse.EMS.Messages.CommitteeMemberPersonalInfoMessages
{
  public class AddCommitteeMemberPersonalInfoResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public AddCommitteeMemberPersonalInfoResponseMessage(ValidationResult validationResult)
    {
      ValidationResult = validationResult;
    }
  }
}
