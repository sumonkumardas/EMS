using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.CommitteeMemberMessages;

namespace SinePulse.EMS.UseCases.CommitteeMembers
{
  public class ShowCommitteeMemberRequestHandler : IRequestHandler<ShowCommitteeMemberRequestMessage, ShowCommitteeMemberResponseMessage>
  {
    private readonly ShowCommitteeMemberRequestMessageValidator _validator;
    private readonly IShowCommitteeMemberUseCase _useCase;

    public ShowCommitteeMemberRequestHandler(ShowCommitteeMemberRequestMessageValidator validator,
      IShowCommitteeMemberUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ShowCommitteeMemberResponseMessage> Handle(ShowCommitteeMemberRequestMessage request,
      CancellationToken cancellationToken)
    {
      ShowCommitteeMemberResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ShowCommitteeMemberResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var committeeMember = _useCase.GetCommitteeMember(request);

      returnMessage = new ShowCommitteeMemberResponseMessage(validationResult, committeeMember);
      return Task.FromResult(returnMessage);
    }
  }
}