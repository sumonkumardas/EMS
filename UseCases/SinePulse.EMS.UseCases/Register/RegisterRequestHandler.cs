using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.RegisterMessages;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.UseCases.Register
{
  public class RegisterRequestHandler : IRequestHandler<RegisterRequestMessage, ValidatedData<RegisterResponseMessage>>
  {
    private readonly RegisterRequestMessageValidator _validator;
    private readonly IRegisterUseCase _useCase;


    public RegisterRequestHandler(RegisterRequestMessageValidator validator,
      IRegisterUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ValidatedData<RegisterResponseMessage>> Handle(RegisterRequestMessage request,
      CancellationToken cancellationToken)
    {
      ValidatedData<RegisterResponseMessage> returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ValidatedData<RegisterResponseMessage>(validationResult);
        return Task.FromResult(returnMessage);
      }

      var response = _useCase.Register(request);

      returnMessage = new ValidatedData<RegisterResponseMessage>(validationResult, response);
      return Task.FromResult(returnMessage);
    }
  }
}