using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.ClassMessages;

namespace SinePulse.EMS.UseCases.Classes
{
  public class AddClassRequestHandler : IRequestHandler<AddClassRequestMessage, AddClassResponseMessage>
  {
    private readonly AddClassRequestMessageValidator _validator;
    private readonly IAddClassUseCase _useCase;

    public AddClassRequestHandler(AddClassRequestMessageValidator validator, IAddClassUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<AddClassResponseMessage> Handle(AddClassRequestMessage request, CancellationToken cancellationToken)
    {
      AddClassResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new AddClassResponseMessage(validationResult);
        return Task.FromResult(returnMessage);
      }

      var @class =_useCase.AddClass(request);

      returnMessage = new AddClassResponseMessage(validationResult, @class.Id);
      return Task.FromResult(returnMessage);
    }
  }
}