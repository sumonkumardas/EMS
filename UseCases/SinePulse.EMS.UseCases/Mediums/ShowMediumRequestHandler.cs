using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.MediumMessages;

namespace SinePulse.EMS.UseCases.Mediums
{
  public class ShowMediumRequestHandler : IRequestHandler<ShowMediumRequestMessage, ShowMediumResponseMessage>
  {
    private readonly ShowMediumRequestMessageValidator _validator;
    private readonly IShowMediumUseCase _useCase;

    public ShowMediumRequestHandler(ShowMediumRequestMessageValidator validator, IShowMediumUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ShowMediumResponseMessage> Handle(ShowMediumRequestMessage request,
      CancellationToken cancellationToken)
    {
      ShowMediumResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ShowMediumResponseMessage(validationResult,null);
        return Task.FromResult(returnMessage);
      }

      var medium = _useCase.ShowMedium(request);

      returnMessage = new ShowMediumResponseMessage(validationResult, medium);
      return Task.FromResult(returnMessage);
    }
  }
}