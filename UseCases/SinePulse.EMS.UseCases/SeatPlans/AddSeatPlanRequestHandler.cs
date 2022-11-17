using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.SeatPlanMessages;
using SinePulse.EMS.Messages.Shared;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.SeatPlans
{
  public class AddSeatPlanRequestHandler : IRequestHandler<AddSeatPlanRequestMessage, ValidatedData<AddSeatPlanResponseMessage>>
  {
    private readonly AddSeatPlanRequestMessageValidator _validator;
    private readonly IAddSeatPlanUseCase _useCase;
    private readonly EmsDbContext _dbContext;


    public AddSeatPlanRequestHandler(AddSeatPlanRequestMessageValidator validator,
      IAddSeatPlanUseCase useCase,
      EmsDbContext dbContext)
    {
      _validator = validator;
      _useCase = useCase;
      _dbContext = dbContext;
    }

    public Task<ValidatedData<AddSeatPlanResponseMessage>> Handle(AddSeatPlanRequestMessage request,
      CancellationToken cancellationToken)
    {
      ValidatedData<AddSeatPlanResponseMessage> returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ValidatedData<AddSeatPlanResponseMessage>(validationResult);
        return Task.FromResult(returnMessage);
      }

      var response = _useCase.AddSeatPlan(request);
      _dbContext.SaveChanges();

      returnMessage = new ValidatedData<AddSeatPlanResponseMessage>(validationResult, response);
      return Task.FromResult(returnMessage);
    }
  }
}