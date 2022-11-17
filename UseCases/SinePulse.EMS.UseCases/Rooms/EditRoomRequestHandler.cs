using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.RoomMessages;

namespace SinePulse.EMS.UseCases.Rooms
{
  public class EditRoomRequestHandler : IRequestHandler<EditRoomRequestMessage, EditRoomResponseMessage>
  {
    private readonly EditRoomRequestMessageValidator _validator;
    private readonly IEditRoomUseCase _useCase;

    public EditRoomRequestHandler(EditRoomRequestMessageValidator validator, IEditRoomUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<EditRoomResponseMessage> Handle(EditRoomRequestMessage request, CancellationToken cancellationToken)
    {
      EditRoomResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new EditRoomResponseMessage(validationResult);
        return Task.FromResult(returnMessage);
      }

      _useCase.EditRoom(request);

      returnMessage = new EditRoomResponseMessage(validationResult);
      return Task.FromResult(returnMessage);
    }
  }
  
}