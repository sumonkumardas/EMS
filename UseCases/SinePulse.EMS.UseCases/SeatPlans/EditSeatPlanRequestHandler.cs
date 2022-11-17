using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.SeatPlanMessages;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.UseCases.SeatPlans
{
  public class
    EditSeatPlanRequestHandler : IRequestHandler<EditSeatPlanRequestMessage, ValidatedData<EditSeatPlanResponseMessage>>
  {
    private readonly EditSeatPlanRequestMessageValidator _validator;
    private readonly IEditSeatPlanUseCase _useCase;


    public EditSeatPlanRequestHandler(EditSeatPlanRequestMessageValidator validator,
      IEditSeatPlanUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ValidatedData<EditSeatPlanResponseMessage>> Handle(EditSeatPlanRequestMessage request,
      CancellationToken cancellationToken)
    {
      ValidatedData<EditSeatPlanResponseMessage> returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ValidatedData<EditSeatPlanResponseMessage>(validationResult);
        return Task.FromResult(returnMessage);
      }

      var response = _useCase.EditSeatPlan(request);

      returnMessage = new ValidatedData<EditSeatPlanResponseMessage>(validationResult, response);
      return Task.FromResult(returnMessage);
    }
  }
}