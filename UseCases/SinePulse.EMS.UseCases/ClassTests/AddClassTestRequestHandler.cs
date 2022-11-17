using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.ClassTestMessages;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.ClassTests
{
  public class AddClassTestRequestHandler : IRequestHandler<AddClassTestRequestMessage, AddClassTestResponseMessage>
  {
    private readonly IAddClassTestUseCase _useCase;
    private readonly AddClassTestRequestMessageValidator _validator;
    private readonly EmsDbContext _dbContext;

    public AddClassTestRequestHandler(IAddClassTestUseCase useCase, AddClassTestRequestMessageValidator validator,
      EmsDbContext dbContext)
    {
      _useCase = useCase;
      _validator = validator;
      _dbContext = dbContext;
    }

    public Task<AddClassTestResponseMessage> Handle(AddClassTestRequestMessage request,
      CancellationToken cancellationToken)
    {
      AddClassTestResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new AddClassTestResponseMessage(validationResult);
        return Task.FromResult(returnMessage);
      }

      var classTest = _useCase.AddClassTest(request);
      _dbContext.SaveChanges();

      returnMessage = new AddClassTestResponseMessage(validationResult, classTest.Id);
      return Task.FromResult(returnMessage);
    }
  }
}