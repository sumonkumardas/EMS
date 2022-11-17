using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.OnlinePaymentMessages;

namespace SinePulse.EMS.UseCases.OnlinePayments
{
  public class GetUserInfoRequestHandler : IRequestHandler<GetUserInfoRequestMessage, GetUserInfoResponseMessage>
  {
    private readonly GetUserInfoRequestMessageValidator _validator;
    private readonly IGetUserInfoUseCase _useCase;

    public GetUserInfoRequestHandler(GetUserInfoRequestMessageValidator validator, IGetUserInfoUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<GetUserInfoResponseMessage> Handle(GetUserInfoRequestMessage request,
      CancellationToken cancellationToken)
    {
      GetUserInfoResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new GetUserInfoResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var userInfo = _useCase.GetUserInfo(request);

      returnMessage = new GetUserInfoResponseMessage(validationResult, userInfo);
      return Task.FromResult(returnMessage);
    }
  }
}