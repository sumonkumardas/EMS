using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.ShiftMessages;

namespace SinePulse.EMS.UseCases.Shifts
{
  public class EditShiftRequestHandler : IRequestHandler<EditShiftRequestMessage, EditShiftResponseMessage>
  {
    private readonly EditShiftRequestMessageValidator _validator;
    private readonly IEditShiftUseCase _useCase;

    public EditShiftRequestHandler(EditShiftRequestMessageValidator validator, IEditShiftUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<EditShiftResponseMessage> Handle(EditShiftRequestMessage request,
      CancellationToken cancellationToken)
    {
      EditShiftResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new EditShiftResponseMessage(validationResult);
        return Task.FromResult(returnMessage);
      }

      _useCase.UpdateShift(request);

      returnMessage = new EditShiftResponseMessage(validationResult);
      return Task.FromResult(returnMessage);
    }
  }
}