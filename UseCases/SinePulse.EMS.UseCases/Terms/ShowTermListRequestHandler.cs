using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.Shared;
using SinePulse.EMS.Messages.TermMessages;

namespace SinePulse.EMS.UseCases.Terms
{
  public class ShowTermListRequestHandler : IRequestHandler<ShowTermListRequestMessage, ValidatedData<ShowTermListResponseMessage>>
  {
    private readonly ShowTermListRequestMessageValidator _validator;
    private readonly IShowTermListUseCase _useCase;

    public ShowTermListRequestHandler(ShowTermListRequestMessageValidator validator, IShowTermListUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ValidatedData<ShowTermListResponseMessage>> Handle(ShowTermListRequestMessage request,
      CancellationToken cancellationToken)
    {
      ValidatedData<ShowTermListResponseMessage> returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ValidatedData<ShowTermListResponseMessage>(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var responseMessage = _useCase.ShowTermList(request);

      returnMessage = new ValidatedData<ShowTermListResponseMessage>(validationResult, responseMessage);
      return Task.FromResult(returnMessage);
    }
  }
}