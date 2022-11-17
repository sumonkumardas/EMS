using FluentValidation;
using SinePulse.EMS.Messages.CommitteeMemberPersonalInfoMessages;

namespace SinePulse.EMS.UseCases.CommitteeMemberPersonalInfos
{
  public class GetCommitteeMemberPersonalInfoRequestMessageValidator : AbstractValidator<GetCommitteeMemberPersonalInfoRequestMessage>
  {
    public GetCommitteeMemberPersonalInfoRequestMessageValidator()
    {

    }
  }
}
