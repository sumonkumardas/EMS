using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.AttendanceMessages;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Employee
{
  public class ShowEmployeeAttendanceRequestHandler : IRequestHandler<ShowEmployeeAttendanceRequestMessage, ShowEmployeeAttendanceResponseMessage>
  {
    private readonly ShowEmployeeAttendanceRequestMessageValidator _validator;
    private readonly IShowEmployeeAttendanceUseCase _useCase;
    private readonly EmsDbContext _dbContext;

    public ShowEmployeeAttendanceRequestHandler(ShowEmployeeAttendanceRequestMessageValidator validator, IShowEmployeeAttendanceUseCase useCase,
      EmsDbContext dbContext)
    {
      _validator = validator;
      _useCase = useCase;
      _dbContext = dbContext;
    }

    public Task<ShowEmployeeAttendanceResponseMessage> Handle(ShowEmployeeAttendanceRequestMessage request,
      CancellationToken cancellationToken)
    {
      ShowEmployeeAttendanceResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ShowEmployeeAttendanceResponseMessage(validationResult,null);
        return Task.FromResult(returnMessage);
      }

       var employee = _useCase.ShowEmployeeAttendance(request);

      returnMessage = new ShowEmployeeAttendanceResponseMessage(validationResult, employee);
      return Task.FromResult(returnMessage);
    }
  }
}