using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.LoginUserMessages;

namespace SinePulse.EMS.UseCases.LoginUsers
{
  public class ChangeLoginUserPasswordRequestHandler : IRequestHandler<ChangeLoginUserPasswordRequestMessage,
    ChangeLoginUserPasswordResponseMessage>
  {
    private readonly ChangeLoginUserPasswordRequestMessageValidator _validator;
    private readonly IChangeLoginUserPasswordUseCase _useCase;

    public ChangeLoginUserPasswordRequestHandler(ChangeLoginUserPasswordRequestMessageValidator validator,
      IChangeLoginUserPasswordUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ChangeLoginUserPasswordResponseMessage> Handle(ChangeLoginUserPasswordRequestMessage request,
      CancellationToken cancellationToken)
    {
      ChangeLoginUserPasswordResponseMessage returnMessage;
      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ChangeLoginUserPasswordResponseMessage(validationResult);
        return Task.FromResult(returnMessage);
      }

      _useCase.ChangeLoginUserPassword(request);

      returnMessage = new ChangeLoginUserPasswordResponseMessage(validationResult);
      return Task.FromResult(returnMessage);
    }
  }
}