using FluentValidation;
using SinePulse.EMS.Messages.CommitteeMemberMessages;

namespace SinePulse.EMS.UseCases.CommitteeMembers
{
  public class ShowCommitteeMemberRequestMessageValidator : AbstractValidator<ShowCommitteeMemberRequestMessage>
  {
    public ShowCommitteeMemberRequestMessageValidator()
    {
    }
  }
}