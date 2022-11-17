using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.SessionMessages;

namespace SinePulse.EMS.UseCases.Sessions
{
  public class GetBranchMediumSessionsRequestHandler : IRequestHandler<GetBranchMediumSessionsRequestMessage, GetBranchMediumSessionsResponseMessage>
  {
    private readonly GetBranchMediumSessionsRequestMessageValidator _validator;
    private readonly IGetBranchMediumSessionsUseCase _useCase;

    public GetBranchMediumSessionsRequestHandler(GetBranchMediumSessionsRequestMessageValidator validator,
      IGetBranchMediumSessionsUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<GetBranchMediumSessionsResponseMessage> Handle(GetBranchMediumSessionsRequestMessage request,
      CancellationToken cancellationToken)
    {
      GetBranchMediumSessionsResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new GetBranchMediumSessionsResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var sessions = _useCase.GetBranchMediumSessions(request);

      returnMessage = new GetBranchMediumSessionsResponseMessage(validationResult, sessions);
      return Task.FromResult(returnMessage);
    }
  }
}