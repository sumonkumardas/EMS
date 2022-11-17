using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.ClassRoutineMessages;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.ClassRoutines
{
  public class ShowClassRoutineListRequestHandler : IRequestHandler<ShowClassRoutineListRequestMessage, ShowClassRoutineListResponseMessage>
  {
    private readonly ShowClassRoutineListRequestMessageValidator _validator;
    private readonly IShowClassRoutineListUseCase _useCase;
    private readonly EmsDbContext _dbContext;

    public ShowClassRoutineListRequestHandler(ShowClassRoutineListRequestMessageValidator validator, IShowClassRoutineListUseCase useCase,
      EmsDbContext dbContext)
    {
      _validator = validator;
      _useCase = useCase;
      _dbContext = dbContext;
    }

    public Task<ShowClassRoutineListResponseMessage> Handle(ShowClassRoutineListRequestMessage request,
      CancellationToken cancellationToken)
    {
      ShowClassRoutineListResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ShowClassRoutineListResponseMessage(validationResult,null);
        return Task.FromResult(returnMessage);
      }

       var classRoutineList = _useCase.ShowClassRoutineList(request);

      returnMessage = new ShowClassRoutineListResponseMessage(validationResult, classRoutineList);
      return Task.FromResult(returnMessage);
    }
  }
}