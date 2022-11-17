using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.ClassTestMessage;
using SinePulse.EMS.Messages.ClassTestMessages;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.UseCases.ClassTests
{
  public class DisplayAddClassTestPageRequestHandler
    : IRequestHandler<DisplayAddClassTestPageRequestMessage,
      ValidatedData<DisplayAddClassTestPageResponseMessage>>
  {
    private readonly DisplayAddClassTestPageRequestMessageValidator _validator;
    private readonly IDisplayAddClassTestPageUseCase _useCase;


    public DisplayAddClassTestPageRequestHandler(DisplayAddClassTestPageRequestMessageValidator validator,
      IDisplayAddClassTestPageUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ValidatedData<DisplayAddClassTestPageResponseMessage>> Handle(
      DisplayAddClassTestPageRequestMessage request, CancellationToken cancellationToken)
    {
      ValidatedData<DisplayAddClassTestPageResponseMessage> returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ValidatedData<DisplayAddClassTestPageResponseMessage>(validationResult);
        return Task.FromResult(returnMessage);
      }

      var response = _useCase.DisplayAddClassTestPage(request);

      returnMessage = new ValidatedData<DisplayAddClassTestPageResponseMessage>(validationResult, response);
      return Task.FromResult(returnMessage);
    }
  }
}