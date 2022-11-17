using FluentValidation;
using SinePulse.EMS.Messages.RoomMessages;

namespace SinePulse.EMS.UseCases.Rooms
{
  public class ShowRoomsListRequestMessageValidator : AbstractValidator<ShowRoomsListRequestMessage>
  {
    public ShowRoomsListRequestMessageValidator()
    {
    }
  }
}