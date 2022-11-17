using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.SeatPlanMessages;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.UseCases.SeatPlans
{
  public class ShowSeatPlanRequestHandler : IRequestHandler<ShowSeatPlanRequestMessage, ValidatedData<ShowSeatPlanResponseMessage>>
  {
    private readonly ShowSeatPlanRequestMessageValidator _validator;
    private readonly IShowSeatPlanUseCase _useCase;

    public ShowSeatPlanRequestHandler(ShowSeatPlanRequestMessageValidator validator, IShowSeatPlanUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ValidatedData<ShowSeatPlanResponseMessage>> Handle(ShowSeatPlanRequestMessage request,
      CancellationToken cancellationToken)
    {
      ValidatedData<ShowSeatPlanResponseMessage> returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ValidatedData<ShowSeatPlanResponseMessage>(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var responseMessage = _useCase.ShowSeatPlan(request);

      returnMessage = new ValidatedData<ShowSeatPlanResponseMessage>(validationResult, responseMessage);
      return Task.FromResult(returnMessage);
    }
  }
}