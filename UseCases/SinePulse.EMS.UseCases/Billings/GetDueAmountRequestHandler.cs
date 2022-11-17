using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.BillingMessages;

namespace SinePulse.EMS.UseCases.Billings
{
  public class GetDueAmountRequestHandler : IRequestHandler<GetDueAmountRequestMessage, GetDueAmountResponseMessage>
  {
    private readonly GetDueAmountRequestMessageValidator _validator;
    private readonly IGetDueAmountUseCase _useCase;

    public GetDueAmountRequestHandler(GetDueAmountRequestMessageValidator validator,
      IGetDueAmountUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<GetDueAmountResponseMessage> Handle(GetDueAmountRequestMessage request,
      CancellationToken cancellationToken)
    {
      GetDueAmountResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new GetDueAmountResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var pendingInfo = _useCase.GetDueAmount(request);

      returnMessage = new GetDueAmountResponseMessage(validationResult, pendingInfo);
      return Task.FromResult(returnMessage);
    }
  }
}