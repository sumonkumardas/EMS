using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.EmployeeMessages;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Employee
{
  public class ShowEmployeeRequestHandler : IRequestHandler<ShowEmployeeRequestMessage, ShowEmployeeResponseMessage>
  {
    private readonly ShowEmployeeRequestMessageValidator _validator;
    private readonly IShowEmployeeUseCase _useCase;
    private readonly EmsDbContext _dbContext;

    public ShowEmployeeRequestHandler(ShowEmployeeRequestMessageValidator validator, IShowEmployeeUseCase useCase,
      EmsDbContext dbContext)
    {
      _validator = validator;
      _useCase = useCase;
      _dbContext = dbContext;
    }

    public Task<ShowEmployeeResponseMessage> Handle(ShowEmployeeRequestMessage request,
      CancellationToken cancellationToken)
    {
      ShowEmployeeResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ShowEmployeeResponseMessage(validationResult,null);
        return Task.FromResult(returnMessage);
      }

       var employee = _useCase.ShowEmployee(request);

      returnMessage = new ShowEmployeeResponseMessage(validationResult, employee);
      return Task.FromResult(returnMessage);
    }
  }
}