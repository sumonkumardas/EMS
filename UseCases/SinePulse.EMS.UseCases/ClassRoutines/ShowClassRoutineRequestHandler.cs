using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.ClassRoutineMessages;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.ClassRoutines
{
  public class ShowClassRoutineRequestHandler : IRequestHandler<ShowClassRoutineRequestMessage, ShowClassRoutineResponseMessage>
  {
    private readonly ShowClassRoutineRequestMessageValidator _validator;
    private readonly IShowClassRoutineUseCase _useCase;
    private readonly EmsDbContext _dbContext;

    public ShowClassRoutineRequestHandler(ShowClassRoutineRequestMessageValidator validator, IShowClassRoutineUseCase useCase,
      EmsDbContext dbContext)
    {
      _validator = validator;
      _useCase = useCase;
      _dbContext = dbContext;
    }

    public Task<ShowClassRoutineResponseMessage> Handle(ShowClassRoutineRequestMessage request,
      CancellationToken cancellationToken)
    {
      ShowClassRoutineResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ShowClassRoutineResponseMessage(validationResult,null);
        return Task.FromResult(returnMessage);
      }

       var classRoutine = _useCase.ShowClassRoutine(request);

      returnMessage = new ShowClassRoutineResponseMessage(validationResult, classRoutine);
      return Task.FromResult(returnMessage);
    }
  }
}