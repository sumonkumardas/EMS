using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.AttendanceMessages;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Attendances
{
  public class ShowCurrentMonthAttendanceListRequestHandler : IRequestHandler<ShowCurrentMonthAttendanceListRequestMessage,
    ShowCurrentMonthAttendanceListResponseMessage>
  {
    private readonly ShowCurrentMonthAttendanceListRequestMessageValidator _validator;
    private readonly IShowCurrentMonthAttendanceListUseCase _useCase;

    public ShowCurrentMonthAttendanceListRequestHandler(ShowCurrentMonthAttendanceListRequestMessageValidator validator,
      IShowCurrentMonthAttendanceListUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ShowCurrentMonthAttendanceListResponseMessage> Handle(ShowCurrentMonthAttendanceListRequestMessage request,
      CancellationToken cancellationToken)
    {
      ShowCurrentMonthAttendanceListResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ShowCurrentMonthAttendanceListResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var employeeAttendance = _useCase.ShowEmployeeAttendanceList(request);

      returnMessage = new ShowCurrentMonthAttendanceListResponseMessage(validationResult, employeeAttendance);
      return Task.FromResult(returnMessage);
    }
  }
}