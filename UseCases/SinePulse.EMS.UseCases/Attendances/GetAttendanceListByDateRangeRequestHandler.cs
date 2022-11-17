using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.AttendanceMessages;

namespace SinePulse.EMS.UseCases.Attendances
{
  public class GetAttendanceListByDateRangeRequestHandler : IRequestHandler<
    GetAttendanceListByDateRangeRequestMessage, GetAttendanceListByDateRangeResponseMessage>
  {
    private readonly GetAttendanceListByDateRangeRequestMessageValidator _validator;
    private readonly IGetAttendanceListByDateRangeUseCase _useCase;

    public GetAttendanceListByDateRangeRequestHandler(
      GetAttendanceListByDateRangeRequestMessageValidator validator,
      IGetAttendanceListByDateRangeUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<GetAttendanceListByDateRangeResponseMessage> Handle(
      GetAttendanceListByDateRangeRequestMessage request,
      CancellationToken cancellationToken)
    {
      GetAttendanceListByDateRangeResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new GetAttendanceListByDateRangeResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var attendanceList = _useCase.GetEmployeeAttendanceListByDateRange(request);

      returnMessage = new GetAttendanceListByDateRangeResponseMessage(validationResult, attendanceList);
      return Task.FromResult(returnMessage);
    }
  }
}