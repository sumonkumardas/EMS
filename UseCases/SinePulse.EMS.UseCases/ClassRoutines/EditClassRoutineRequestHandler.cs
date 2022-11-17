using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.ClassRoutineMessages;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.ClassRoutines
{
  public class EditClassRoutineRequestHandler
    : IRequestHandler<EditClassRoutineRequestMessage, EditClassRoutineResponseMessage>
  {
    private readonly EditClassRoutineRequestMessageValidator _validator;
    private readonly IEditClassRoutineUseCase _useCase;
    private readonly EmsDbContext _dbContext;

    public EditClassRoutineRequestHandler(EditClassRoutineRequestMessageValidator validator,
      IEditClassRoutineUseCase useCase,
      EmsDbContext dbContext)
    {
      _validator = validator;
      _useCase = useCase;
      _dbContext = dbContext;
    }

    public Task<EditClassRoutineResponseMessage> Handle(EditClassRoutineRequestMessage request,
      CancellationToken cancellationToken)
    {
      EditClassRoutineResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new EditClassRoutineResponseMessage(validationResult);
        return Task.FromResult(returnMessage);
      }

      _useCase.EditClassRoutine(request);
      _dbContext.SaveChanges();

      returnMessage = new EditClassRoutineResponseMessage(validationResult);
      return Task.FromResult(returnMessage);
    }
  }
}