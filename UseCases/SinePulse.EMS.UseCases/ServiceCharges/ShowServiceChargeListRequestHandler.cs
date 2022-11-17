using MediatR;
using SinePulse.EMS.Messages.ServiceChargeMessages;
using SinePulse.EMS.Messages.Shared;
using System.Threading;
using System.Threading.Tasks;

namespace SinePulse.EMS.UseCases.ServiceCharges
{
  public class ShowServiceChargeListRequestHandler : IRequestHandler<ShowServiceChargeListRequestMessage, ValidatedData<ShowServiceChargeListResponseMessage>>
  {
    private readonly ShowServiceChargeListRequestMessageValidator _validator;
    private readonly IShowServiceChargeListUseCase _useCase;

    public ShowServiceChargeListRequestHandler(ShowServiceChargeListRequestMessageValidator validator, IShowServiceChargeListUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ValidatedData<ShowServiceChargeListResponseMessage>> Handle(ShowServiceChargeListRequestMessage request,
      CancellationToken cancellationToken)
    {
      ValidatedData<ShowServiceChargeListResponseMessage> returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ValidatedData<ShowServiceChargeListResponseMessage>(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var responseMessage = _useCase.ShowServiceChargeList(request);

      returnMessage = new ValidatedData<ShowServiceChargeListResponseMessage>(validationResult, responseMessage);
      return Task.FromResult(returnMessage);
    }
  }
}
