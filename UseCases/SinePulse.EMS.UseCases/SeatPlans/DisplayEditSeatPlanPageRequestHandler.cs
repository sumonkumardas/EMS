using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.SeatPlanMessages;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.UseCases.SeatPlans
{
  public class DisplayEditSeatPlanPageRequestHandler
    : IRequestHandler<DisplayEditSeatPlanPageRequestMessage, ValidatedData<DisplayEditSeatPlanPageResponseMessage>>
  {
    private readonly DisplayEditSeatPlanPageRequestMessageValidator _validator;
    private readonly IDisplayEditSeatPlanPageUseCase _useCase;


    public DisplayEditSeatPlanPageRequestHandler(DisplayEditSeatPlanPageRequestMessageValidator validator,
      IDisplayEditSeatPlanPageUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ValidatedData<DisplayEditSeatPlanPageResponseMessage>> Handle(
      DisplayEditSeatPlanPageRequestMessage request,
      CancellationToken cancellationToken)
    {
      ValidatedData<DisplayEditSeatPlanPageResponseMessage> returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ValidatedData<DisplayEditSeatPlanPageResponseMessage>(validationResult);
        return Task.FromResult(returnMessage);
      }

      var response = _useCase.DisplayEditSeatPlanPage(request);

      returnMessage = new ValidatedData<DisplayEditSeatPlanPageResponseMessage>(validationResult, response);
      return Task.FromResult(returnMessage);
    }
  }
}