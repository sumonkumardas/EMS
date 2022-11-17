using FluentValidation.Results;

namespace SinePulse.EMS.Messages.RoomMessages
{
  public class EditRoomResponseMessage
  {
    public ValidationResult ValidationResult { get; }

    public EditRoomResponseMessage(ValidationResult validationResult)
    {
      ValidationResult = validationResult;
    }
  }
}