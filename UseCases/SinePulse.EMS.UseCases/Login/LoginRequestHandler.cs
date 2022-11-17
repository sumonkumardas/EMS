using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.LoginMessages;
using SinePulse.EMS.Messages.Shared;
using SinePulse.EMS.UseCases.Login;

namespace SinePulse.EMS.UseCases.Login
{
  public class LoginRequestHandler : IRequestHandler<LoginRequestMessage, ValidatedData<LoginResponseMessage>>
  {
    private readonly LoginRequestMessageValidator _validator;
    private readonly ILoginUseCase _useCase;


    public LoginRequestHandler(LoginRequestMessageValidator validator,
      ILoginUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ValidatedData<LoginResponseMessage>> Handle(LoginRequestMessage request,
      CancellationToken cancellationToken)
    {
      ValidatedData<LoginResponseMessage> returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ValidatedData<LoginResponseMessage>(validationResult);
        return Task.FromResult(returnMessage);
      }

      var response = _useCase.Login(request);

      returnMessage = new ValidatedData<LoginResponseMessage>(validationResult, response);
      return Task.FromResult(returnMessage);
    }
  }
}