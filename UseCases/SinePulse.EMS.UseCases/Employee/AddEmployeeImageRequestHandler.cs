using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.EmployeeMessages;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Employee
{
  public class AddEmployeeImageRequestHandler : IRequestHandler<AddEmployeeImageRequestMessage, AddEmployeeImageResponseMessage>
  {
    private readonly AddEmployeeImageRequestMessageValidator _validator;
    private readonly IAddEmployeeImageUseCase _useCase;
    private readonly EmsDbContext _dbContext;

    public AddEmployeeImageRequestHandler(AddEmployeeImageRequestMessageValidator validator, IAddEmployeeImageUseCase useCase,
      EmsDbContext dbContext)
    {
      _validator = validator;
      _useCase = useCase;
      _dbContext = dbContext;
    }

    public Task<AddEmployeeImageResponseMessage> Handle(AddEmployeeImageRequestMessage request, CancellationToken cancellationToken)
    {
      AddEmployeeImageResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new AddEmployeeImageResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var previousImage = _useCase.UploadEmployeeImage(request);
      _dbContext.SaveChanges();

      returnMessage = new AddEmployeeImageResponseMessage(validationResult, previousImage);
      return Task.FromResult(returnMessage);
    }
  }
}