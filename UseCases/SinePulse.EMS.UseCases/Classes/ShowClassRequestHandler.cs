using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.ClassMessages;

namespace SinePulse.EMS.UseCases.Classes
{
  public class ShowClassRequestHandler : IRequestHandler<ShowClassRequestMessage, ShowClassResponseMessage>
  {
    
    private readonly ShowClassRequestMessageValidator _validator;
    private readonly IShowClassUseCase _useCase;

    public ShowClassRequestHandler(ShowClassRequestMessageValidator validator, IShowClassUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ShowClassResponseMessage> Handle(ShowClassRequestMessage request,
      CancellationToken cancellationToken)
    {
      ShowClassResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ShowClassResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var @class = _useCase.GetClass(request);

      returnMessage = new ShowClassResponseMessage(validationResult, @class);
      return Task.FromResult(returnMessage);
    }
  }
}