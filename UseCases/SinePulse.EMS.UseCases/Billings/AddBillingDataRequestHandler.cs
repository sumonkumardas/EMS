using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.BillingMessages;

namespace SinePulse.EMS.UseCases.Billings
{
  public class AddBillingDataRequestHandler : IRequestHandler<AddBillingDataRequestMessage, AddBillingDataResponseMessage>
  {
    private readonly AddBillingDataRequestMessageValidator _validator;
    private readonly IAddBillingDataUseCase _useCase;

    public AddBillingDataRequestHandler(AddBillingDataRequestMessageValidator validator,
      IAddBillingDataUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<AddBillingDataResponseMessage> Handle(AddBillingDataRequestMessage request,
      CancellationToken cancellationToken)
    {
      AddBillingDataResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new AddBillingDataResponseMessage(validationResult);
        return Task.FromResult(returnMessage);
      }

       _useCase.AddBillingData(request);

      returnMessage = new AddBillingDataResponseMessage(validationResult);
      return Task.FromResult(returnMessage);
    }
  }
}