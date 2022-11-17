using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.Shared;
using SinePulse.EMS.Messages.TermTestMessages;

namespace SinePulse.EMS.UseCases.TermTests
{
  public class DisplayAddTermTestPageRequestHandler
    : IRequestHandler<DisplayAddTermTestPageRequestMessage, ValidatedData<DisplayAddTermTestPageResponseMessage>>
  {
    private readonly DisplayAddTermTestPageRequestMessageValidator _validator;
    private readonly IDisplayAddTermTestPageUseCase _useCase;


    public DisplayAddTermTestPageRequestHandler(DisplayAddTermTestPageRequestMessageValidator validator,
      IDisplayAddTermTestPageUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ValidatedData<DisplayAddTermTestPageResponseMessage>> Handle(DisplayAddTermTestPageRequestMessage request,
      CancellationToken cancellationToken)
    {
      ValidatedData<DisplayAddTermTestPageResponseMessage> returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ValidatedData<DisplayAddTermTestPageResponseMessage>(validationResult);
        return Task.FromResult(returnMessage);
      }

      var response = _useCase.DisplayAddTermTestPage(request);

      returnMessage = new ValidatedData<DisplayAddTermTestPageResponseMessage>(validationResult, response);
      return Task.FromResult(returnMessage);
    }
  }
}