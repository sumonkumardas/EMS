using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.RoomMessages;

namespace SinePulse.EMS.UseCases.Rooms
{
  public class ShowRoomsListRequestHandler : IRequestHandler<ShowRoomsListRequestMessage, ShowRoomsListResponseMessage>
  {
    private readonly ShowRoomsListRequestMessageValidator _validator;
    private readonly IShowRoomsListUseCase _useCase;

    public ShowRoomsListRequestHandler(ShowRoomsListRequestMessageValidator validator, IShowRoomsListUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ShowRoomsListResponseMessage> Handle(ShowRoomsListRequestMessage request,
      CancellationToken cancellationToken)
    {
      ShowRoomsListResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ShowRoomsListResponseMessage(validationResult,null);
        return Task.FromResult(returnMessage);
      }

      var roomsList = _useCase.ShowRoomsList(request);

      returnMessage = new ShowRoomsListResponseMessage(validationResult, roomsList);
      return Task.FromResult(returnMessage);
    }
  }
}