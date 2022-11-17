using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.Shared;
using SinePulse.EMS.Messages.YearClosingMessages;

namespace SinePulse.EMS.UseCases.YearClosings
{
  public class YearClosingRequestHandler
    : IRequestHandler<YearClosingRequestMessage, ValidatedData<YearClosingResponseMessage>>
  {
    private readonly YearClosingRequestMessageValidator _validator;
    private readonly IYearClosingUseCase _useCase;

    public YearClosingRequestHandler(YearClosingRequestMessageValidator validator,
      IYearClosingUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ValidatedData<YearClosingResponseMessage>> Handle(YearClosingRequestMessage request,
      CancellationToken cancellationToken)
    {
      ValidatedData<YearClosingResponseMessage> returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ValidatedData<YearClosingResponseMessage>(validationResult);
        return Task.FromResult(returnMessage);
      }

      var response = _useCase.YearClosing(request);

      returnMessage =
        new ValidatedData<YearClosingResponseMessage>(validationResult, response);
      return Task.FromResult(returnMessage);
    }
  }
}