using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.SessionMessages;

namespace SinePulse.EMS.UseCases.Sessions
{
  public class ShowSessionRequestHandler : IRequestHandler<ViewSessionRequestMessage, ViewSessionResponseMessage>
  {
    private readonly ShowSessionRequestMessageValidator _validator;
    private readonly IShowSessionUseCase _useCase;

    public ShowSessionRequestHandler(ShowSessionRequestMessageValidator validator, IShowSessionUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ViewSessionResponseMessage> Handle(ViewSessionRequestMessage request,
      CancellationToken cancellationToken)
    {
      ViewSessionResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ViewSessionResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var session = _useCase.ShowSession(request);

      returnMessage = new ViewSessionResponseMessage(validationResult, session);
      return Task.FromResult(returnMessage);
    }
  }
}