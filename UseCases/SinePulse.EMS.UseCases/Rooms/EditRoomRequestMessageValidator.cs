using FluentValidation;
using SinePulse.EMS.Messages.RoomMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.Rooms
{
  public class EditRoomRequestMessageValidator : AbstractValidator<EditRoomRequestMessage>
  {
    private readonly IUniqueRoomPropertyChecker _uniqueRoomPropertyChecker;

    public EditRoomRequestMessageValidator(IUniqueRoomPropertyChecker uniqueRoomPropertyChecker)
    {
      _uniqueRoomPropertyChecker = uniqueRoomPropertyChecker;
      RuleFor(x => x.RoomNo).NotEmpty().WithMessage("Please specify Room No.");
      RuleFor(x => x.RoomNo).NotNull().WithMessage("Please specify Room No.");
      RuleFor(x => x.RoomNo).Must((m, x) => IsUniqueRoomNo(x, m.BranchId, m.Id)).WithMessage("Room No already exists.");
    }

    private bool IsUniqueRoomNo(string roomNo, long branchId, long roomId)
    {
      return _uniqueRoomPropertyChecker.IsUniqueRoomNo(roomNo, branchId, roomId);
    }
  }
}