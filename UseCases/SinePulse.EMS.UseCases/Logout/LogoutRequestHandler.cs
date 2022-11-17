using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.LogoutMessages;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.UseCases.Logout
{
  public class LogoutRequestHandler : IRequestHandler<LogoutRequestMessage, ValidatedData<LogoutResponseMessage>>
  {
    private readonly LogoutRequestMessageValidator _validator;
    private readonly ILogoutUseCase _useCase;


    public LogoutRequestHandler(LogoutRequestMessageValidator validator,
      ILogoutUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ValidatedData<LogoutResponseMessage>> Handle(LogoutRequestMessage request,
      CancellationToken cancellationToken)
    {
      ValidatedData<LogoutResponseMessage> returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ValidatedData<LogoutResponseMessage>(validationResult);
        return Task.FromResult(returnMessage);
      }

      var response = _useCase.Logout(request);

      returnMessage = new ValidatedData<LogoutResponseMessage>(validationResult, response);
      return Task.FromResult(returnMessage);
    }
  }
}