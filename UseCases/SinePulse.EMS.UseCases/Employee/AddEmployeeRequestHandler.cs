using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.EmployeeMessages;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Employee
{
  public class AddEmployeeRequestHandler : IRequestHandler<AddEmployeeRequestMessage, AddEmployeeResponseMessage>
  {
    private readonly AddEmployeeRequestMessageValidator _validator;
    private readonly IAddEmployeeUseCase _useCase;
    private readonly EmsDbContext _dbContext;

    public AddEmployeeRequestHandler(AddEmployeeRequestMessageValidator validator, IAddEmployeeUseCase useCase,
      EmsDbContext dbContext)
    {
      _validator = validator;
      _useCase = useCase;
      _dbContext = dbContext;
    }

    public Task<AddEmployeeResponseMessage> Handle(AddEmployeeRequestMessage request, CancellationToken cancellationToken)
    {
      AddEmployeeResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new AddEmployeeResponseMessage(validationResult,0);
        return Task.FromResult(returnMessage);
      }

      var employee = _useCase.AddEmployee(request);
      _dbContext.SaveChanges();

      returnMessage = new AddEmployeeResponseMessage(validationResult,employee.Id);
      return Task.FromResult(returnMessage);
    }
  }
}