using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.SeatPlanMessages;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.UseCases.SeatPlans
{
  public class DisplayAddSeatPlanPageRequestHandler
    : IRequestHandler<DisplayAddSeatPlanPageRequestMessage, ValidatedData<DisplayAddSeatPlanPageResponseMessage>>
  {
    private readonly DisplayAddSeatPlanPageRequestMessageValidator _validator;
    private readonly IDisplayAddSeatPlanPageUseCase _useCase;


    public DisplayAddSeatPlanPageRequestHandler(DisplayAddSeatPlanPageRequestMessageValidator validator,
      IDisplayAddSeatPlanPageUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ValidatedData<DisplayAddSeatPlanPageResponseMessage>> Handle(DisplayAddSeatPlanPageRequestMessage request,
      CancellationToken cancellationToken)
    {
      ValidatedData<DisplayAddSeatPlanPageResponseMessage> returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ValidatedData<DisplayAddSeatPlanPageResponseMessage>(validationResult);
        return Task.FromResult(returnMessage);
      }

      var response = _useCase.DisplayAddSeatPlanPage(request);

      returnMessage = new ValidatedData<DisplayAddSeatPlanPageResponseMessage>(validationResult, response);
      return Task.FromResult(returnMessage);
    }
  }
}