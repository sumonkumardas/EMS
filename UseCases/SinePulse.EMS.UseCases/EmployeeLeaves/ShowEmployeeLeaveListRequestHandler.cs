using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.EmployeeLeaveMessages;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.EmployeeLeaves
{
  public class ShowEmployeeLeaveListRequestHandler : IRequestHandler<ShowEmployeeLeaveListRequestMessage, ShowEmployeeLeaveListResponseMessage>
  {
    private readonly ShowEmployeeLeaveListRequestMessageValidator _validator;
    private readonly IShowEmployeeLeaveListUseCase _useCase;
    private readonly EmsDbContext _dbContext;

    public ShowEmployeeLeaveListRequestHandler(ShowEmployeeLeaveListRequestMessageValidator validator, IShowEmployeeLeaveListUseCase useCase,
      EmsDbContext dbContext)
    {
      _validator = validator;
      _useCase = useCase;
      _dbContext = dbContext;
    }

    public Task<ShowEmployeeLeaveListResponseMessage> Handle(ShowEmployeeLeaveListRequestMessage request, CancellationToken cancellationToken)
    {
      ShowEmployeeLeaveListResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ShowEmployeeLeaveListResponseMessage(validationResult,null);
        return Task.FromResult(returnMessage);
      }

      var employeeLeaveList = _useCase.ShowUnaprrovedLeaveList(request);

      returnMessage = new ShowEmployeeLeaveListResponseMessage(validationResult, employeeLeaveList);
      return Task.FromResult(returnMessage);
    }
  }
}