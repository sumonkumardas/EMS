using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.LoginUserMessages;

namespace SinePulse.EMS.UseCases.LoginUsers
{
  public class AddLoginUserRequestHandler : IRequestHandler<AddLoginUserRequestMessage, AddLoginUserResponseMessage>
  {
    private readonly AddLoginUserRequestMessageValidator _validator;
    private readonly IAddLoginUserUseCase _useCase;

    public AddLoginUserRequestHandler(AddLoginUserRequestMessageValidator validator, IAddLoginUserUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<AddLoginUserResponseMessage> Handle(AddLoginUserRequestMessage request,
      CancellationToken cancellationToken)
    {
      AddLoginUserResponseMessage returnMessage;
      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new AddLoginUserResponseMessage(validationResult);
        return Task.FromResult(returnMessage);
      }

      _useCase.AddLoginUser(request);

      returnMessage = new AddLoginUserResponseMessage(validationResult);
      return Task.FromResult(returnMessage);
    }
  }
}