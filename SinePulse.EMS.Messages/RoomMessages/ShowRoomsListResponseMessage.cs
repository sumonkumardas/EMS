using System.Collections.Generic;
using FluentValidation.Results;
using SinePulse.EMS.Messages.Model.Examinations;

namespace SinePulse.EMS.Messages.RoomMessages
{
  public class ShowRoomsListResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public List<RoomMessageModel> RoomList { get; }

    public ShowRoomsListResponseMessage(ValidationResult validationResult, List<RoomMessageModel> roomList)
    {
      ValidationResult = validationResult;
      RoomList = roomList;
    }
  }
}                                                         