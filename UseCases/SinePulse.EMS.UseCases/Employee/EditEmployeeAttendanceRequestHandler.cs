using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.AttendanceMessages;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Employee
{
  public class EditEmployeeAttendanceRequestHandler : IRequestHandler<EditEmployeeAttendanceRequestMessage, EditEmployeeAttendanceResponseMessage>
  {
    private readonly EditEmployeeAttendanceRequestMessageValidator _validator;
    private readonly IEditEmployeeAttendanceUseCase _useCase;
    private readonly EmsDbContext _dbContext;

    public EditEmployeeAttendanceRequestHandler(EditEmployeeAttendanceRequestMessageValidator validator, IEditEmployeeAttendanceUseCase useCase,
      EmsDbContext dbContext)
    {
      _validator = validator;
      _useCase = useCase;
      _dbContext = dbContext;
    }

    public Task<EditEmployeeAttendanceResponseMessage> Handle(EditEmployeeAttendanceRequestMessage request, CancellationToken cancellationToken)
    {
      EditEmployeeAttendanceResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new EditEmployeeAttendanceResponseMessage(validationResult);
        return Task.FromResult(returnMessage);
      }

      _useCase.EditEmployeeAttendance(request);
      _dbContext.SaveChanges();

      returnMessage = new EditEmployeeAttendanceResponseMessage(validationResult);
      return Task.FromResult(returnMessage);
    }
  }
}