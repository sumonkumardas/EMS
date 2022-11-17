using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.ShiftMessages;

namespace SinePulse.EMS.UseCases.Shifts
{
  public class ShowShiftListRequestHandler : IRequestHandler<ShowShiftListRequestMessage, ShowShiftListResponseMessage>
  {
    private readonly ShowShiftListRequestMessageValidator _validator;
    private readonly IShowShiftListUseCase _useCase;

    public ShowShiftListRequestHandler(ShowShiftListRequestMessageValidator validator, IShowShiftListUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ShowShiftListResponseMessage> Handle(ShowShiftListRequestMessage request,
      CancellationToken cancellationToken)
    {
      ShowShiftListResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ShowShiftListResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var shifts = _useCase.ShowShiftList(request);

      returnMessage = new ShowShiftListResponseMessage(validationResult, shifts);
      return Task.FromResult(returnMessage);
    }
  }
}