using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.ClassTestMessages;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.ClassTests
{
  public class EditClassTestRequestHandler : IRequestHandler<EditClassTestRequestMessage, EditClassTestResponseMessage>
  {
    private readonly IEditClassTestUseCase _useCase;
    private readonly EditClassTestRequestMessageValidator _validator;
    private readonly EmsDbContext _dbContext;

    public EditClassTestRequestHandler(IEditClassTestUseCase useCase, EditClassTestRequestMessageValidator validator,
      EmsDbContext dbContext)
    {
      _useCase = useCase;
      _validator = validator;
      _dbContext = dbContext;
    }

    public Task<EditClassTestResponseMessage> Handle(EditClassTestRequestMessage request,
      CancellationToken cancellationToken)
    {
      EditClassTestResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new EditClassTestResponseMessage(validationResult);
        return Task.FromResult(returnMessage);
      }

      _useCase.EditClassTest(request);
      _dbContext.SaveChanges();

      returnMessage = new EditClassTestResponseMessage(validationResult);
      return Task.FromResult(returnMessage);
    }
  }
}