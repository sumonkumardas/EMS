using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.RoomMessages;

namespace SinePulse.EMS.UseCases.Rooms
{
  public class ShowRoomRequestHandler : IRequestHandler<ShowRoomRequestMessage, ShowRoomResponseMessage>
  {
    private readonly ShowRoomRequestMessageValidator _validator;
    private readonly IShowRoomUseCase _useCase;

    public ShowRoomRequestHandler(ShowRoomRequestMessageValidator validator, IShowRoomUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ShowRoomResponseMessage> Handle(ShowRoomRequestMessage request, CancellationToken cancellationToken)
    {
      ShowRoomResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ShowRoomResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var roomMessageModel = _useCase.ShowRoom(request);

      returnMessage = new ShowRoomResponseMessage(validationResult, roomMessageModel);
      return Task.FromResult(returnMessage);
    }
  }
}