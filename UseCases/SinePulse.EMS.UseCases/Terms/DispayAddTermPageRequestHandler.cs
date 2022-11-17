using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.Shared;
using SinePulse.EMS.Messages.TermMessages;

namespace SinePulse.EMS.UseCases.Terms
{
  public class DisplayAddTermPageRequestHandler
    : IRequestHandler<DisplayAddTermPageRequestMessage, ValidatedData<DisplayAddTermPageResponseMessage>>
  {
    private readonly DisplayAddTermPageRequestMessageValidator _validator;
    private readonly IDisplayAddTermPageUseCase _useCase;


    public DisplayAddTermPageRequestHandler(DisplayAddTermPageRequestMessageValidator validator,
      IDisplayAddTermPageUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ValidatedData<DisplayAddTermPageResponseMessage>> Handle(DisplayAddTermPageRequestMessage request,
      CancellationToken cancellationToken)
    {
      ValidatedData<DisplayAddTermPageResponseMessage> returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ValidatedData<DisplayAddTermPageResponseMessage>(validationResult);
        return Task.FromResult(returnMessage);
      }

      var response = _useCase.DisplayAddTermPage(request);

      returnMessage = new ValidatedData<DisplayAddTermPageResponseMessage>(validationResult, response);
      return Task.FromResult(returnMessage);
    }
  }
}