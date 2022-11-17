using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.TermTestMessages;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.TermTests
{
  public class AddTermTestRequestHandler
    : IRequestHandler<AddTermTestRequestMessage, AddTermTestResponseMessage>
  {
    private readonly IAddTermTestUseCase _useCase;
    private readonly AddTermTestRequestMessageValidator _validator;
    private readonly EmsDbContext _dbContext;

    public AddTermTestRequestHandler(IAddTermTestUseCase useCase,
      AddTermTestRequestMessageValidator validator,
      EmsDbContext dbContext)
    {
      _useCase = useCase;
      _validator = validator;
      _dbContext = dbContext;
    }

    public Task<AddTermTestResponseMessage> Handle(AddTermTestRequestMessage request,
      CancellationToken cancellationToken)
    {
      AddTermTestResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new AddTermTestResponseMessage(validationResult);
        return Task.FromResult(returnMessage);
      }

      _useCase.AddTermTest(request);
      _dbContext.SaveChanges();

      returnMessage = new AddTermTestResponseMessage(validationResult);
      return Task.FromResult(returnMessage);
    }
  }
}