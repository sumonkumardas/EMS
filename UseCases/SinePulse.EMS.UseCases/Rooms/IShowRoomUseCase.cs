using SinePulse.EMS.Messages.Model.Examinations;
using SinePulse.EMS.Messages.RoomMessages;

namespace SinePulse.EMS.UseCases.Rooms
{
  public interface IShowRoomUseCase
  {
    RoomMessageModel ShowRoom(ShowRoomRequestMessage request);  
  }
}