using FluentValidation.Results;
using SinePulse.EMS.Messages.Model.Examinations;

namespace SinePulse.EMS.Messages.RoomMessages
{
  public class ShowRoomResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public RoomMessageModel Room { get; }

    public ShowRoomResponseMessage(ValidationResult validationResult, RoomMessageModel room)
    {
      ValidationResult = validationResult;
      Room = room;
    }
  }
}