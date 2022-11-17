using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.MarkMessages;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.UseCases.Marks
{
  public class DisplayAddMarkPageRequestHandler
    : IRequestHandler<DisplayAddMarkPageRequestMessage, ValidatedData<DisplayAddMarkPageResponseMessage>>
  {
    private readonly DisplayAddMarkPageRequestMessageValidator _validator;
    private readonly IDisplayAddMarkPageUseCase _useCase;


    public DisplayAddMarkPageRequestHandler(DisplayAddMarkPageRequestMessageValidator validator,
      IDisplayAddMarkPageUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ValidatedData<DisplayAddMarkPageResponseMessage>> Handle(DisplayAddMarkPageRequestMessage request,
      CancellationToken cancellationToken)
    {
      ValidatedData<DisplayAddMarkPageResponseMessage> returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ValidatedData<DisplayAddMarkPageResponseMessage>(validationResult);
        return Task.FromResult(returnMessage);
      }

      var response = _useCase.DisplayAddMarkPage(request);

      returnMessage = new ValidatedData<DisplayAddMarkPageResponseMessage>(validationResult, response);
      return Task.FromResult(returnMessage);
    }
  }
}