using SinePulse.EMS.Messages.RoomMessages;

namespace SinePulse.EMS.UseCases.Rooms
{
  public interface IEditRoomUseCase
  {
    void EditRoom(EditRoomRequestMessage message);
  }
}