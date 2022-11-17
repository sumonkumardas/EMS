using FluentValidation.Results;

namespace SinePulse.EMS.Messages.RoomMessages
{
  public class AddRoomResponseMessage
  {
    public ValidationResult ValidationResult { get; }

    public AddRoomResponseMessage(ValidationResult validationResult)
    {
      ValidationResult = validationResult;
    }
  }
}