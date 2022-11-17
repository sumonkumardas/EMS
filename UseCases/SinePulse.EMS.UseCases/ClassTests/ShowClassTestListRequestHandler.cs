using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.ClassTestMessages;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.UseCases.ClassTests
{
  public class ShowClassTestListRequestHandler : IRequestHandler<ShowClassTestListRequestMessage, ValidatedData<ShowClassTestListResponseMessage>>
  {
    private readonly ShowClassTestListRequestMessageValidator _validator;
    private readonly IShowClassTestListUseCase _useCase;

    public ShowClassTestListRequestHandler(ShowClassTestListRequestMessageValidator validator, IShowClassTestListUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ValidatedData<ShowClassTestListResponseMessage>> Handle(ShowClassTestListRequestMessage request,
      CancellationToken cancellationToken)
    {
      ValidatedData<ShowClassTestListResponseMessage> returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ValidatedData<ShowClassTestListResponseMessage>(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var responseMessage = _useCase.ShowClassTestList(request);

      returnMessage = new ValidatedData<ShowClassTestListResponseMessage>(validationResult, responseMessage);
      return Task.FromResult(returnMessage);
    }
  }
}