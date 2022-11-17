using FluentValidation;
using SinePulse.EMS.Messages.SessionMessages;

namespace SinePulse.EMS.UseCases.Sessions
{
  public class GetBranchMediumSessionsRequestMessageValidator : AbstractValidator<GetBranchMediumSessionsRequestMessage>
  {
    public GetBranchMediumSessionsRequestMessageValidator()
    {
    }
  }
}