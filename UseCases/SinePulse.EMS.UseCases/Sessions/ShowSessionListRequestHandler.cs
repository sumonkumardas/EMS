using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.SessionMessages;

namespace SinePulse.EMS.UseCases.Sessions
{
  public class ShowSessionListRequestHandler : IRequestHandler<ShowSessionListRequestMessage, ShowSessionListResponseMessage>
  {
    private readonly ShowSessionListRequestMessageValidator _validator;
    private readonly IShowSessionListUseCase _useCase;

    public ShowSessionListRequestHandler(ShowSessionListRequestMessageValidator validator,
      IShowSessionListUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ShowSessionListResponseMessage> Handle(ShowSessionListRequestMessage request,
      CancellationToken cancellationToken)
    {
      ShowSessionListResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ShowSessionListResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var sessions = _useCase.ShowSessionList(request);

      returnMessage = new ShowSessionListResponseMessage(validationResult, sessions);
      return Task.FromResult(returnMessage);
    }
  }
}