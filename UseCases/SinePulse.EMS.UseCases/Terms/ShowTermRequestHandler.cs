using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.Shared;
using SinePulse.EMS.Messages.TermMessages;

namespace SinePulse.EMS.UseCases.Terms
{
  public class ShowTermRequestHandler : IRequestHandler<ShowTermRequestMessage, ValidatedData<ShowTermResponseMessage>>
  {
    private readonly ShowTermRequestMessageValidator _validator;
    private readonly IShowTermUseCase _useCase;

    public ShowTermRequestHandler(ShowTermRequestMessageValidator validator, IShowTermUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ValidatedData<ShowTermResponseMessage>> Handle(ShowTermRequestMessage request,
      CancellationToken cancellationToken)
    {
      ValidatedData<ShowTermResponseMessage> returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ValidatedData<ShowTermResponseMessage>(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var responseMessage = _useCase.ShowTerm(request);

      returnMessage = new ValidatedData<ShowTermResponseMessage>(validationResult, responseMessage);
      return Task.FromResult(returnMessage);
    }
  }
}