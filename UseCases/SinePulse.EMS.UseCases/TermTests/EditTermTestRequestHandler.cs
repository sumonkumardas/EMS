using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.TermTestMessages;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.TermTests
{
  public class EditTermTestRequestHandler
    : IRequestHandler<EditTermTestRequestMessage, EditTermTestResponseMessage>
  {
    private readonly IEditTermTestUseCase _useCase;
    private readonly EditTermTestRequestMessageValidator _validator;
    private readonly EmsDbContext _dbContext;

    public EditTermTestRequestHandler(IEditTermTestUseCase useCase,
      EditTermTestRequestMessageValidator validator,
      EmsDbContext dbContext)
    {
      _useCase = useCase;
      _validator = validator;
      _dbContext = dbContext;
    }

    public Task<EditTermTestResponseMessage> Handle(EditTermTestRequestMessage request,
      CancellationToken cancellationToken)
    {
      EditTermTestResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new EditTermTestResponseMessage(validationResult);
        return Task.FromResult(returnMessage);
      }

      _useCase.EditTermTest(request);
      _dbContext.SaveChanges();

      returnMessage = new EditTermTestResponseMessage(validationResult);
      return Task.FromResult(returnMessage);
    }
  }
}