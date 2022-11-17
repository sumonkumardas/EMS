using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.MediumMessages;

namespace SinePulse.EMS.UseCases.Mediums
{
  public class AddMediumRequestHandler : IRequestHandler<AddMediumRequestMessage, AddMediumResponseMessage>
  {
    private readonly AddMediumRequestMessageValidator _validator;
    private readonly IAddMediumUseCase _useCase;

    public AddMediumRequestHandler(AddMediumRequestMessageValidator validator, IAddMediumUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<AddMediumResponseMessage> Handle(AddMediumRequestMessage request, CancellationToken cancellationToken)
    {
      AddMediumResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new AddMediumResponseMessage(validationResult);
        return Task.FromResult(returnMessage);
      }

      var medium = _useCase.AddMedium(request);

      returnMessage = new AddMediumResponseMessage(validationResult, medium.Id);
      return Task.FromResult(returnMessage);
    }
  }
}