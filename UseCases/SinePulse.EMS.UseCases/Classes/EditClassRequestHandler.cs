using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.ClassMessages;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Classes
{
  public class EditClassRequestHandler : IRequestHandler<EditClassRequestMessage, EditClassResponseMessage>
  {
    private readonly EditClassRequestMessageValidator _validator;
    private readonly IEditClassUseCase _useCase;
    private readonly EmsDbContext _dbContext;

    public EditClassRequestHandler(EditClassRequestMessageValidator validator, IEditClassUseCase useCase,
      EmsDbContext dbContext)
    {
      _validator = validator;
      _useCase = useCase;
      _dbContext = dbContext;
    }

    public Task<EditClassResponseMessage> Handle(EditClassRequestMessage request,
      CancellationToken cancellationToken)
    {
      EditClassResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new EditClassResponseMessage(validationResult);
        return Task.FromResult(returnMessage);
      }

      _useCase.EditClass(request);
      _dbContext.SaveChanges();

      returnMessage = new EditClassResponseMessage(validationResult);
      return Task.FromResult(returnMessage);
    }
  }
}