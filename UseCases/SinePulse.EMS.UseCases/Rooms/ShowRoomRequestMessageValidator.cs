using FluentValidation;
using SinePulse.EMS.Messages.RoomMessages;

namespace SinePulse.EMS.UseCases.Rooms
{
  public class ShowRoomRequestMessageValidator : AbstractValidator<ShowRoomRequestMessage>
  {
    public ShowRoomRequestMessageValidator()
    {
    }
  }
}