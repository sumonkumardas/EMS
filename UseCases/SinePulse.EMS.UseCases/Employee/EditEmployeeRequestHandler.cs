using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.EmployeeMessages;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Employee
{
  public class EditEmployeeRequestHandler : IRequestHandler<EditEmployeeRequestMessage, EditEmployeeResponseMessage>
  {
    private readonly EditEmployeeRequestMessageValidator _validator;
    private readonly IEditEmployeeUseCase _useCase;
    private readonly EmsDbContext _dbContext;

    public EditEmployeeRequestHandler(EditEmployeeRequestMessageValidator validator, IEditEmployeeUseCase useCase,
      EmsDbContext dbContext)
    {
      _validator = validator;
      _useCase = useCase;
      _dbContext = dbContext;
    }

    public Task<EditEmployeeResponseMessage> Handle(EditEmployeeRequestMessage request, CancellationToken cancellationToken)
    {
      EditEmployeeResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new EditEmployeeResponseMessage(validationResult);
        return Task.FromResult(returnMessage);
      }

      _useCase.EditEmployee(request);
      _dbContext.SaveChanges();

      returnMessage = new EditEmployeeResponseMessage(validationResult);
      return Task.FromResult(returnMessage);
    }
  }
}