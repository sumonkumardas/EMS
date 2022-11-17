using MediatR;
using SinePulse.EMS.Messages.ServiceChargeMessages;
using SinePulse.EMS.Messages.Shared;
using System.Threading;
using System.Threading.Tasks;

namespace SinePulse.EMS.UseCases.ServiceCharges
{
  public class DisplayEditServiceChargeRequestHandler : IRequestHandler<DisplayEditServiceChargeRequestMessage, ValidatedData<DisplayEditServiceChargeResponseMessage>>
  {
    private readonly DisplayEditServiceChargeRequestMessageValidator _validator;
    private readonly IDisplayEditServiceChargeUseCase _useCase;

    public DisplayEditServiceChargeRequestHandler(DisplayEditServiceChargeRequestMessageValidator validator, IDisplayEditServiceChargeUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ValidatedData<DisplayEditServiceChargeResponseMessage>> Handle(DisplayEditServiceChargeRequestMessage request,
      CancellationToken cancellationToken)
    {
      ValidatedData<DisplayEditServiceChargeResponseMessage> returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ValidatedData<DisplayEditServiceChargeResponseMessage>(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var responseMessage = _useCase.ShowServiceCharge(request);

      returnMessage = new ValidatedData<DisplayEditServiceChargeResponseMessage>(validationResult, responseMessage);
      return Task.FromResult(returnMessage);
    }
  }
}
