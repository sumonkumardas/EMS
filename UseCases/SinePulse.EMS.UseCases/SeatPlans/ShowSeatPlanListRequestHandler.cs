using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.SeatPlanMessages;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.UseCases.SeatPlans
{
  public class ShowSeatPlanListRequestHandler : IRequestHandler<ShowSeatPlanListRequestMessage, ValidatedData<ShowSeatPlanListResponseMessage>>
  {
    private readonly ShowSeatPlanListRequestMessageValidator _validator;
    private readonly IShowSeatPlanListUseCase _useCase;

    public ShowSeatPlanListRequestHandler(ShowSeatPlanListRequestMessageValidator validator, IShowSeatPlanListUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ValidatedData<ShowSeatPlanListResponseMessage>> Handle(ShowSeatPlanListRequestMessage request,
      CancellationToken cancellationToken)
    {
      ValidatedData<ShowSeatPlanListResponseMessage> returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ValidatedData<ShowSeatPlanListResponseMessage>(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var responseMessage = _useCase.ShowSeatPlanList(request);

      returnMessage = new ValidatedData<ShowSeatPlanListResponseMessage>(validationResult, responseMessage);
      return Task.FromResult(returnMessage);
    }
  }
}