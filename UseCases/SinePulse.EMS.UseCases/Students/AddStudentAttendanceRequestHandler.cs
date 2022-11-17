using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.AttendanceMessages;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Students
{
  public class AddStudentAttendanceRequestHandler : IRequestHandler<AddStudentAttendanceRequestMessage, AddStudentAttendanceResponseMessage>
  {
    private readonly AddStudentAttendanceRequestMessageValidator _validator;
    private readonly IAddStudentAttendanceUseCase _useCase;
    private readonly EmsDbContext _dbContext;

    public AddStudentAttendanceRequestHandler(AddStudentAttendanceRequestMessageValidator validator, IAddStudentAttendanceUseCase useCase,
      EmsDbContext dbContext)
    {
      _validator = validator;
      _useCase = useCase;
      _dbContext = dbContext;
    }

    public Task<AddStudentAttendanceResponseMessage> Handle(AddStudentAttendanceRequestMessage request, CancellationToken cancellationToken)
    {
      AddStudentAttendanceResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new AddStudentAttendanceResponseMessage(validationResult,0);
        return Task.FromResult(returnMessage);
      }

      var studentAttendance = _useCase.AddStudentAttendance(request);
      _dbContext.SaveChanges();

      returnMessage = new AddStudentAttendanceResponseMessage(validationResult, studentAttendance.Id);
      return Task.FromResult(returnMessage);
    }
  }
}