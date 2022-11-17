using FluentValidation;
using SinePulse.EMS.Messages.CommitteeMemberMessages;

namespace SinePulse.EMS.UseCases.CommitteeMembers
{
  public class AddOrChangeCommitteeMemberImageRequestMessageValidator : AbstractValidator<
      AddOrChangeCommitteeMemberImageRequestMessage>
  {
    public AddOrChangeCommitteeMemberImageRequestMessageValidator()
    {
    }
  }
}