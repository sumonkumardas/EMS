using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.MediumMessages;

namespace SinePulse.EMS.UseCases.Mediums
{
  public class ShowMediumListRequestHandler : IRequestHandler<ShowMediumListRequestMessage, ShowMediumListResponseMessage>
  {
    private readonly ShowMediumListRequestMessageValidator _validator;
    private readonly IShowMediumListUseCase _useCase;

    public ShowMediumListRequestHandler(ShowMediumListRequestMessageValidator validator,
      IShowMediumListUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ShowMediumListResponseMessage> Handle(ShowMediumListRequestMessage request,
      CancellationToken cancellationToken)
    {
      ShowMediumListResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ShowMediumListResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var mediums = _useCase.ShowMediumList(request);

      returnMessage = new ShowMediumListResponseMessage(validationResult, mediums);
      return Task.FromResult(returnMessage);
    }
  }
}