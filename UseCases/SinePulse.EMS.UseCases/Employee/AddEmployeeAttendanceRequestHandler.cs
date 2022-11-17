using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.AttendanceMessages;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Employee
{
  public class AddEmployeeAttendanceRequestHandler : IRequestHandler<AddEmployeeAttendanceRequestMessage, AddEmployeeAttendanceResponseMessage>
  {
    private readonly AddEmployeeAttendanceRequestMessageValidator _validator;
    private readonly IAddEmployeeAttendanceUseCase _useCase;
    private readonly EmsDbContext _dbContext;

    public AddEmployeeAttendanceRequestHandler(AddEmployeeAttendanceRequestMessageValidator validator, IAddEmployeeAttendanceUseCase useCase,
      EmsDbContext dbContext)
    {
      _validator = validator;
      _useCase = useCase;
      _dbContext = dbContext;
    }

    public Task<AddEmployeeAttendanceResponseMessage> Handle(AddEmployeeAttendanceRequestMessage request, CancellationToken cancellationToken)
    {
      AddEmployeeAttendanceResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new AddEmployeeAttendanceResponseMessage(validationResult,0);
        return Task.FromResult(returnMessage);
      }

      var employeeAttendance = _useCase.AddEmployeeAttendance(request);
      _dbContext.SaveChanges();

      returnMessage = new AddEmployeeAttendanceResponseMessage(validationResult, employeeAttendance.Id);
      return Task.FromResult(returnMessage);
    }
  }
}