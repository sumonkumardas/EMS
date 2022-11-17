using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.LoginMessages;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.UseCases.Login
{
  public class DisplayLoginPageRequestHandler
    : IRequestHandler<DisplayLoginPageRequestMessage, ValidatedData<DisplayLoginPageResponseMessage>>
  {
    private readonly DisplayLoginPageRequestMessageValidator _validator;
    private readonly IDisplayLoginPageUseCase _useCase;


    public DisplayLoginPageRequestHandler(DisplayLoginPageRequestMessageValidator validator,
      IDisplayLoginPageUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ValidatedData<DisplayLoginPageResponseMessage>> Handle(DisplayLoginPageRequestMessage request,
      CancellationToken cancellationToken)
    {
      ValidatedData<DisplayLoginPageResponseMessage> returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ValidatedData<DisplayLoginPageResponseMessage>(validationResult);
        return Task.FromResult(returnMessage);
      }

      var response = _useCase.DisplayLoginPage(request);

      returnMessage = new ValidatedData<DisplayLoginPageResponseMessage>(validationResult, response);
      return Task.FromResult(returnMessage);
    }
  }
}