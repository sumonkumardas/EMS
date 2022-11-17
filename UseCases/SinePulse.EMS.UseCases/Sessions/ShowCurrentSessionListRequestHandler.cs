using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.SessionMessages;

namespace SinePulse.EMS.UseCases.Sessions
{
  public class ShowCurrentSessionListRequestHandler : IRequestHandler<ShowCurrentSessionListRequestMessage, ShowCurrentSessionListResponseMessage>
  {
    private readonly IShowCurrentSessionListUseCase _useCase;

    public ShowCurrentSessionListRequestHandler(IShowCurrentSessionListUseCase useCase)
    {
      _useCase = useCase;
    }

    public Task<ShowCurrentSessionListResponseMessage> Handle(ShowCurrentSessionListRequestMessage request,
      CancellationToken cancellationToken)
    {
      var sessions = _useCase.ShowSessionList(request.BranchMediumId);

      ShowCurrentSessionListResponseMessage returnMessage = new ShowCurrentSessionListResponseMessage (sessions);
      return Task.FromResult(returnMessage);
    }
  }
}
