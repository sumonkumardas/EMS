using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.SessionMessages;

namespace SinePulse.EMS.UseCases.Sessions
{
  public class AddSessionRequestHandler : IRequestHandler<AddSessionRequestMessage, AddSessionResponseMessage>
  {
    private readonly AddSessionRequestMessageValidator _validator;
    private readonly IAddSessionUseCase _useCase;

    public AddSessionRequestHandler(AddSessionRequestMessageValidator validator, IAddSessionUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<AddSessionResponseMessage> Handle(AddSessionRequestMessage request, CancellationToken cancellationToken)
    {
      AddSessionResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new AddSessionResponseMessage(validationResult);
        return Task.FromResult(returnMessage);
      }

      var session = _useCase.AddSession(request);

      returnMessage = new AddSessionResponseMessage(validationResult, session.Id, request.SessionType, request.ObjectId);
      return Task.FromResult(returnMessage);
    }
  }
}