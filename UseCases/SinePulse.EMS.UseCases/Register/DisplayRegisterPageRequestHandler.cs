using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.RegisterMessages;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.UseCases.Register
{
  public class DisplayRegisterPageRequestHandler
    : IRequestHandler<DisplayRegisterPageRequestMessage, ValidatedData<DisplayRegisterPageResponseMessage>>
  {
    private readonly DisplayRegisterPageRequestMessageValidator _validator;
    private readonly IDisplayRegisterPageUseCase _useCase;


    public DisplayRegisterPageRequestHandler(DisplayRegisterPageRequestMessageValidator validator,
      IDisplayRegisterPageUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ValidatedData<DisplayRegisterPageResponseMessage>> Handle(DisplayRegisterPageRequestMessage request,
      CancellationToken cancellationToken)
    {
      ValidatedData<DisplayRegisterPageResponseMessage> returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ValidatedData<DisplayRegisterPageResponseMessage>(validationResult);
        return Task.FromResult(returnMessage);
      }

      var response = _useCase.DisplayRegisterPage(request);

      returnMessage = new ValidatedData<DisplayRegisterPageResponseMessage>(validationResult, response);
      return Task.FromResult(returnMessage);
    }
  }
}