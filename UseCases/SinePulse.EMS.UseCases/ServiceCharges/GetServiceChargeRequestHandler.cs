using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.ServiceChargeMessages;

namespace SinePulse.EMS.UseCases.ServiceCharges
{
  public class GetServiceChargeRequestHandler : IRequestHandler<GetServiceChargeRequestMessage, GetServiceChargeResponseMessage>
  {
    private readonly GetServiceChargeRequestMessageValidator _validator;
    private readonly IGetServiceChargeUseCase _useCase;

    public GetServiceChargeRequestHandler(GetServiceChargeRequestMessageValidator validator,
      IGetServiceChargeUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }
    public Task<GetServiceChargeResponseMessage> Handle(GetServiceChargeRequestMessage request, CancellationToken cancellationToken)
    {
      GetServiceChargeResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new GetServiceChargeResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var serviceCharge = _useCase.GetServiceCharge(request);

      returnMessage = new GetServiceChargeResponseMessage(validationResult, serviceCharge);
      return Task.FromResult(returnMessage);
    }
  }
}
