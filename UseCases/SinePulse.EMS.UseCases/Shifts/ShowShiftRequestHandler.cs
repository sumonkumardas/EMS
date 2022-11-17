using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.ShiftMessages;

namespace SinePulse.EMS.UseCases.Shifts
{
  public class ShowShiftRequestHandler : IRequestHandler<ShowShiftRequestMessage, ShowShiftResponseMessage>
  {
    private readonly ShowShiftRequestMessageValidator _validator;
    private readonly IShowShiftUseCase _useCase;

    public ShowShiftRequestHandler(ShowShiftRequestMessageValidator validator, IShowShiftUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ShowShiftResponseMessage> Handle(ShowShiftRequestMessage request,
      CancellationToken cancellationToken)
    {
      ShowShiftResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ShowShiftResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var shift = _useCase.ShowShift(request);

      returnMessage = new ShowShiftResponseMessage(validationResult, shift);
      return Task.FromResult(returnMessage);
    }
  }
}