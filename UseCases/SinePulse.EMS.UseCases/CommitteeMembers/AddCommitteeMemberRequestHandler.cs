using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.CommitteeMemberMessages;

namespace SinePulse.EMS.UseCases.CommitteeMembers
{
  public class AddCommitteeMemberRequestHandler : IRequestHandler<AddCommitteeMemberRequestMessage, AddCommitteeMemberResponseMessage>
  {
    private readonly AddCommitteeMemberRequestMessageValidator _validator;
    private readonly IAddCommitteeMemberUseCase _useCase;

    public AddCommitteeMemberRequestHandler(AddCommitteeMemberRequestMessageValidator validator,
      IAddCommitteeMemberUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<AddCommitteeMemberResponseMessage> Handle(AddCommitteeMemberRequestMessage request,
      CancellationToken cancellationToken)
    {
      AddCommitteeMemberResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new AddCommitteeMemberResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var committeeMember = _useCase.AddCommitteeMember(request);

      returnMessage = new AddCommitteeMemberResponseMessage(validationResult, committeeMember);
      return Task.FromResult(returnMessage);
    }
  }
}