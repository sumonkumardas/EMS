using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.AttendanceMessages;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.UseCases.Students;

namespace SinePulse.EMS.UseCases.Students
{
  public class ShowStudentAttendanceRequestHandler : IRequestHandler<ShowStudentAttendanceRequestMessage, ShowStudentAttendanceResponseMessage>
  {
    private readonly ShowStudentAttendanceRequestMessageValidator _validator;
    private readonly IShowStudentAttendanceUseCase _useCase;
    private readonly EmsDbContext _dbContext;

    public ShowStudentAttendanceRequestHandler(ShowStudentAttendanceRequestMessageValidator validator, IShowStudentAttendanceUseCase useCase,
      EmsDbContext dbContext)
    {
      _validator = validator;
      _useCase = useCase;
      _dbContext = dbContext;
    }

    public Task<ShowStudentAttendanceResponseMessage> Handle(ShowStudentAttendanceRequestMessage request,
      CancellationToken cancellationToken)
    {
      ShowStudentAttendanceResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ShowStudentAttendanceResponseMessage(validationResult,null);
        return Task.FromResult(returnMessage);
      }

       var Student = _useCase.ShowStudentAttendance(request);

      returnMessage = new ShowStudentAttendanceResponseMessage(validationResult, Student);
      return Task.FromResult(returnMessage);
    }
  }
}