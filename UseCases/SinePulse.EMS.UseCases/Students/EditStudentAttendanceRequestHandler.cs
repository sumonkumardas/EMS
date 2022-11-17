using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.AttendanceMessages;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Students
{
  public class EditStudentAttendanceRequestHandler : IRequestHandler<EditStudentAttendanceRequestMessage, EditStudentAttendanceResponseMessage>
  {
    private readonly EditStudentAttendanceRequestMessageValidator _validator;
    private readonly IEditStudentAttendanceUseCase _useCase;
    private readonly EmsDbContext _dbContext;

    public EditStudentAttendanceRequestHandler(EditStudentAttendanceRequestMessageValidator validator, IEditStudentAttendanceUseCase useCase,
      EmsDbContext dbContext)
    {
      _validator = validator;
      _useCase = useCase;
      _dbContext = dbContext;
    }

    public Task<EditStudentAttendanceResponseMessage> Handle(EditStudentAttendanceRequestMessage request, CancellationToken cancellationToken)
    {
      EditStudentAttendanceResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new EditStudentAttendanceResponseMessage(validationResult);
        return Task.FromResult(returnMessage);
      }

      _useCase.EditStudentAttendance(request);
      _dbContext.SaveChanges();

      returnMessage = new EditStudentAttendanceResponseMessage(validationResult);
      return Task.FromResult(returnMessage);
    }
  }
}