using System.Threading;
using System.Threading.Tasks;
using SinePulse.EMS.Messages.SessionMessages;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Sessions
{
  public class EditSessionRequestHandler
  {
    private readonly EditSessionRequestMessageValidator _validator;
    private readonly IEditSessionUseCase _useCase;
    private readonly EmsDbContext _dbContext;

    public EditSessionRequestHandler(EditSessionRequestMessageValidator validator, IEditSessionUseCase useCase,
      EmsDbContext dbContext)
    {
      _validator = validator;
      _useCase = useCase;
      _dbContext = dbContext;
    }

    public Task<EditSessionResponseMessage> Handle(EditSessionRequestMessage request, CancellationToken cancellationToken)
    {
      EditSessionResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new EditSessionResponseMessage(validationResult);
        return Task.FromResult(returnMessage);
      }

      _useCase.UpdateSession(request);
      _dbContext.SaveChanges();

      returnMessage = new EditSessionResponseMessage(validationResult);
      return Task.FromResult(returnMessage);
    }
  }
}