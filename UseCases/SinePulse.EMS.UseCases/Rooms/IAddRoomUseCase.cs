using SinePulse.EMS.Messages.RoomMessages;

namespace SinePulse.EMS.UseCases.Rooms
{
  public interface IAddRoomUseCase
  {
    void AddRoom(AddRoomRequestMessage requestMessage);
  }
}