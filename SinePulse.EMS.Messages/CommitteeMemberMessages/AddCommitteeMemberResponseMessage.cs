using FluentValidation.Results;
using SinePulse.EMS.Messages.Model.ManagingCommittee;

namespace SinePulse.EMS.Messages.CommitteeMemberMessages
{
  public class AddCommitteeMemberResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public CommitteeMemberMessageModel CommitteeMember { get; }

    public AddCommitteeMemberResponseMessage(ValidationResult validationResult, CommitteeMemberMessageModel committeeMember)
    {
      ValidationResult = validationResult;
      CommitteeMember = committeeMember;
    }
  }
}