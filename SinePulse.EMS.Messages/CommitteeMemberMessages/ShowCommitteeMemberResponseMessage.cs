using FluentValidation.Results;
using SinePulse.EMS.Messages.Model.ManagingCommittee;

namespace SinePulse.EMS.Messages.CommitteeMemberMessages
{
  public class ShowCommitteeMemberResponseMessage
  {
    public CommitteeMemberMessageModel CommitteeMember { get; }
    public ValidationResult ValidationResult { get; }

    public ShowCommitteeMemberResponseMessage(ValidationResult validationResult,
      CommitteeMemberMessageModel committeeMember)
    {
      ValidationResult = validationResult;
      CommitteeMember = committeeMember;
    }
  }
}