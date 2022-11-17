using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.ClassRoutineMessages;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.ClassRoutines
{
  public class AddClassRoutineRequestHandler
    : IRequestHandler<AddClassRoutineRequestMessage, AddClassRoutineResponseMessage>
  {
    private readonly AddClassRoutineRequestMessageValidator _validator;
    private readonly IAddClassRoutineUseCase _useCase;
    private readonly EmsDbContext _dbContext;

    public AddClassRoutineRequestHandler(AddClassRoutineRequestMessageValidator validator,
      IAddClassRoutineUseCase useCase,
      EmsDbContext dbContext)
    {
      _validator = validator;
      _useCase = useCase;
      _dbContext = dbContext;
    }

    public Task<AddClassRoutineResponseMessage> Handle(AddClassRoutineRequestMessage request,
      CancellationToken cancellationToken)
    {
      AddClassRoutineResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new AddClassRoutineResponseMessage(validationResult, 0);
        return Task.FromResult(returnMessage);
      }

      var classRoutine = _useCase.AddClassRoutine(request);
      _dbContext.SaveChanges();

      returnMessage = new AddClassRoutineResponseMessage(validationResult, classRoutine.Id);
      return Task.FromResult(returnMessage);
    }
  }
}