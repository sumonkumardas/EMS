using MediatR;
using SinePulse.EMS.Messages.ServiceChargeMessages;
using SinePulse.EMS.Messages.Shared;
using System.Threading;
using System.Threading.Tasks;

namespace SinePulse.EMS.UseCases.ServiceCharges
{
  public class EditServiceChargeRequestHandler : IRequestHandler<EditServiceChargeRequestMessage, ValidatedData<EditServiceChargeResponseMessage>>
  {
    private readonly EditServiceChargeRequestMessageValidator _validator;
    private readonly IEditServiceChargeUseCase _useCase;


    public EditServiceChargeRequestHandler(EditServiceChargeRequestMessageValidator validator,
      IEditServiceChargeUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ValidatedData<EditServiceChargeResponseMessage>> Handle(EditServiceChargeRequestMessage request,
      CancellationToken cancellationToken)
    {
      ValidatedData<EditServiceChargeResponseMessage> returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ValidatedData<EditServiceChargeResponseMessage>(validationResult);
        return Task.FromResult(returnMessage);
      }

      var response = _useCase.EditServiceCharge(request);

      returnMessage = new ValidatedData<EditServiceChargeResponseMessage>(validationResult, response);
      return Task.FromResult(returnMessage);
    }
  }
}
