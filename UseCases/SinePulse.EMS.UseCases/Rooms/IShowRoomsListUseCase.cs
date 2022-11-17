using System.Collections.Generic;
using SinePulse.EMS.Messages.Model.Examinations;
using SinePulse.EMS.Messages.RoomMessages;

namespace SinePulse.EMS.UseCases.Rooms
{
  public interface IShowRoomsListUseCase
  {
    List<RoomMessageModel> ShowRoomsList(ShowRoomsListRequestMessage message);
  }
}