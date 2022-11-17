using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.WaiverMessages;

namespace SinePulse.EMS.UseCases.Waivers
{
  public class AddWaiverRequestHandler : IRequestHandler<AddWaiverRequestMessage, AddWaiverResponseMessage>
  {
    private readonly AddWaiverRequestMessageValidator _validator;
    private readonly IAddWaiverUseCase _useCase;

    public AddWaiverRequestHandler(AddWaiverRequestMessageValidator validator, IAddWaiverUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<AddWaiverResponseMessage> Handle(AddWaiverRequestMessage request, CancellationToken cancellationToken)
    {
      AddWaiverResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new AddWaiverResponseMessage(validationResult);
        return Task.FromResult(returnMessage);
      }

      _useCase.AddWaiver(request);

      returnMessage = new AddWaiverResponseMessage(validationResult);
      return Task.FromResult(returnMessage);
    }
  }
}