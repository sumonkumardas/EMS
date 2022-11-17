using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.ClassTestMessages;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.UseCases.ClassTests
{
  public class ShowClassTestRequestHandler : IRequestHandler<ShowClassTestRequestMessage, ValidatedData<ShowClassTestResponseMessage>>
  {
    private readonly ShowClassTestRequestMessageValidator _validator;
    private readonly IShowClassTestUseCase _useCase;

    public ShowClassTestRequestHandler(ShowClassTestRequestMessageValidator validator, IShowClassTestUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ValidatedData<ShowClassTestResponseMessage>> Handle(ShowClassTestRequestMessage request,
      CancellationToken cancellationToken)
    {
      ValidatedData<ShowClassTestResponseMessage> returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ValidatedData<ShowClassTestResponseMessage>(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var responseMessage = _useCase.ShowClassTest(request);

      returnMessage = new ValidatedData<ShowClassTestResponseMessage>(validationResult, responseMessage);
      return Task.FromResult(returnMessage);
    }
  }
}